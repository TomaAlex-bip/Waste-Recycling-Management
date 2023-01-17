using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerApi.Configuration;
using IdentityServerApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WasteRecyclingManagementApi.Core.Repositories;
using WasteRecyclingManagementApi.Data;
using WasteRecyclingManagementApi.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var assembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<RecyclingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("database"));
});

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString("IdentityServerDb"),
                options => options.MigrationsAssembly(assembly));
        };
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString("IdentityServerDb"),
                options => options.MigrationsAssembly(assembly));
        };
    })
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

var app = builder.Build();

//MigrateInMemoryDataToSqlServer(app);

app.UseDeveloperExceptionPage();

app.UseCors("default");

app.UseMiddleware<AuthorizationScopeMiddleware>();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    // configure CORS for angular client
    context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:4200");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
    await next();
});

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



void MigrateInMemoryDataToSqlServer(IApplicationBuilder app)
{
    using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
    {
        if(scope == null)
        {
            return;
        }

        scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        context.Database.Migrate();

        if (!context.Clients.Any())
        {
            foreach (var client in InMemoryConfiguration.Clients())
            {
                context.Clients.Add(client.ToEntity());
            }

            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var apiscope in InMemoryConfiguration.ApiScopes())
            {
                context.ApiScopes.Add(apiscope.ToEntity());
            }

            context.SaveChanges();
        }

    }
}