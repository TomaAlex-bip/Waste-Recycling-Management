using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Data;
using WasteRecyclingManagementApi.Middlewares;
using WasteRecyclingManagementApi.Services;
using WasteRecyclingManagementApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<ContainerListValidator>();
    options.RegisterValidatorsFromAssemblyContaining<RecyclingPointValidator>();
    options.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>();
    options.RegisterValidatorsFromAssemblyContaining<EmployeeOperationValidator>();
    options.RegisterValidatorsFromAssemblyContaining<UserValidator>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<RecyclingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dockerDatabase"));
});

builder.Services.AddHttpContextAccessor();

// add UnitOfWork Dependecy Injection
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// add Services Dependency Injection
builder.Services.AddScoped<ITokenReaderService, TokenReaderService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILocationReaderService, LocationReaderService>();
builder.Services.AddScoped<ILocationWriteService, LocationWriteService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:9999";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsersApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "wasteRecyclingApi.users");
    });

    options.AddPolicy("AdminApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "wasteRecyclingApi.admin");
    });

    options.AddPolicy("EmployeesApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "wasteRecyclingApi.employees");
    });

    options.AddPolicy("PublicApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "wasteRecyclingApi.public");
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.Use(async (context, next) =>
{
    // configure CORS for angular client
    context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:4200");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
    await next();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();


app.Run();
