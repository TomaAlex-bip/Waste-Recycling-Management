using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerApi.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new[]
            {
                new ApiScope("wasteRecyclingApi.users", "Waste Recycling Management For Users"),
                new ApiScope("wasteRecyclingApi.admin", "Waste Recycling Management For Admin"),
                new ApiScope("wasteRecyclingApi.public", "Waste Recycling Management For Public"),
                new ApiScope("wasteRecyclingApi.employees", "Waste Recycling Management For Employees")
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "wasteRecyclingClient",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new []
                    {
                        "wasteRecyclingApi.users",
                        "wasteRecyclingApi.admin", 
                        "wasteRecyclingApi.employees"
                    },
                    AllowedCorsOrigins = new[] { "http://localhost:4200" }
                },

                new Client
                {
                    ClientId = "wasteRecyclingClient.public",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new[]
                    {
                        "wasteRecyclingApi.public"
                    },
                    AllowedCorsOrigins = new[] { "http://localhost:4200" }
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "gigel",
                    Password = "1234",

                }
            };
        }
    }
}
