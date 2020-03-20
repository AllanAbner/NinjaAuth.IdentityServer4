using IdentityServer4.Models;
using System.Collections.Generic;

namespace BankOfDotNet.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("bankOfDotNetApi","Customer  Api for BankOfDotNet")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes  = { GrantType.ClientCredentials },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                }
            };
        }
    }
}