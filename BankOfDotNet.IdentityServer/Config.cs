using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace BankOfDotNet.IdentityServer
{
    public class Config
    {
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Allan",
                    Password= "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Caah",
                    Password= "password"
                }
            };
        }

        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("bankOfDotNetApi","Customer  Api for BankOfDotNet"),
                new ApiResource("sigavapi","API SIGAVI")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 new Client()
                {
                    ClientId = "client-sigav",
                    AllowedGrantTypes  = { GrantType.ClientCredentials },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                   // AllowedScopes = { "sigavapi", "bankOfDotNetApi" },
                    AllowedScopes = { "bankOfDotNetApi", "sigavapi" },
                },
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes  = { GrantType.ClientCredentials },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNetApi", "sigavapi" },
                },

                //Resource Owner grant type
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = {GrantType.ResourceOwnerPassword },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "bankOfDotNetApi" }
                },

                 new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://localhost:53445/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:53445/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                 new Client
                 {
                     ClientId = "swagapi",
                    ClientName = "Swagger API",
                     AllowedGrantTypes = GrantTypes.Implicit,
                     AllowAccessTokensViaBrowser =true,
                      RequireClientSecret = false,
                     RedirectUris = { "http://localhost:5001/swagger/oauth2-redirect.html" },
                     PostLogoutRedirectUris = { "http://localhost:5001/swagger" },
                     AllowedCorsOrigins = {"http://localhost:5001"},

                     AllowedScopes = { "bankOfDotNetApi" }
                 }
            };
        }
    }
}