using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace SunttelTradePointB.Identity
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[] {
                new Client{
                    ClientId = "apiClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "pruebaAPI" }
                },

                new Client{
                    ClientId = "prueba_webassembly",
                    ClientName = "Prueba WebAssembly",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:7213" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "pruebaAPI"
                    },
                    RedirectUris = { "https://localhost:7213/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:7213/authentication/logout-callback" }
                                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("pruebaAPI", "Prueba API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {

           };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[] {

                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "993ea190-e75d-4a73-869e-50ecfec1f3c7",
                    Username = "jorgei",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Jorge"),
                        new Claim(JwtClaimTypes.FamilyName, "Isaza"),
                        new Claim(JwtClaimTypes.Name, "Jorge Isaza"),
                        new Claim(JwtClaimTypes.Email, "jorgeisazac@sunttelsoftware.com")
                    }
                }
            };
    }
}
