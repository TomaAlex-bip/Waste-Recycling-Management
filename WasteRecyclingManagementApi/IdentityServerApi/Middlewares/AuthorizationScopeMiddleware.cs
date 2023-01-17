using System.Text;
using WasteRecyclingManagementApi.Core.Repositories;

namespace IdentityServerApi.Middlewares
{
    public class AuthorizationScopeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationScopeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var requestPath = context.Request.Path.Value;
            if (requestPath == "/connect/token")
            {
                var flow = context.Request.Form["grant_type"];
                if(flow == "password")
                {
                    var username = context.Request.Form["username"];
                    var requestedScope = context.Request.Form["scope"];

                    var user = await userRepository.GetUserAsync(username);

                    if (user != null)
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine($"username: {username}");
                        Console.WriteLine($"role: {user.Role}");
                        Console.WriteLine($"requested scope: {requestedScope}");
                        Console.WriteLine("------------------------------------");

                        if (user.Role == 0 && requestedScope == "wasteRecyclingApi.users")
                        {
                            await _next(context);
                        }
                        else if (user.Role == 1 && requestedScope == "wasteRecyclingApi.employees wasteRecyclingApi.users")
                        {
                            await _next(context);
                        }
                        else if (user.Role == 2 && requestedScope == "wasteRecyclingApi.admin wasteRecyclingApi.users")
                        {
                            await _next(context);
                        }
                        else
                        {
                            var errorMessage = "Scope not permitted for user!";
                            var message = Encoding.UTF8.GetBytes(errorMessage);
                            context.Response.StatusCode = 401;
                            await context.Response.Body.WriteAsync(message, 0, message.Length);
                        }
                    }
                    else
                    {
                        var errorMessage = "Scope not permitted for user!";
                        var message = Encoding.UTF8.GetBytes(errorMessage);
                        context.Response.StatusCode = 401;
                        await context.Response.Body.WriteAsync(message, 0, message.Length);
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
