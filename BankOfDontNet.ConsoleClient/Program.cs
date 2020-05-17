using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankOfDontNet.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //discover all the endpoints using metadata identity server
            var msg = DateTime.Now.AddMinutes(18);

            var dif = DateTime.Now.Subtract(msg).TotalMinutes;

            var httpClientRo = new HttpClient();

            var discoRO = await httpClientRo.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (discoRO.IsError)
            {
                System.Console.WriteLine(discoRO.Error);
                return;
            }
            // Grab a Bearer Token using ResouceOwnerPassword grant type

            var tokenclientPro = new TokenClient(httpClientRo, new TokenClientOptions
            {
                Address = discoRO.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret"
            });

            var tokenResponseRo = await tokenclientPro.RequestPasswordTokenAsync
                ("Caah", "password", "bankOfDotNetApi");

            try
            {
                if (tokenResponseRo.IsError)
                {
                    System.Console.WriteLine(tokenResponseRo.ErrorDescription);
                    return;
                }
                System.Console.WriteLine(tokenResponseRo.Json);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            var httpClient = new HttpClient();

            var discovery = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (discovery.IsError)
            {
                System.Console.WriteLine(discovery.Error);
                return;
            }

            using var tokenRequest = new TokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Parameters =
                {
                    { "scope", "bankOfDotNetApi" },
                    { "grant_type", "client_credentials" }
                }
            };
            var tokenResponse = await httpClient.RequestTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                System.Console.WriteLine(tokenResponse.ErrorDescription);
                return;
            }
            System.Console.WriteLine(tokenResponse.Json);
        }
    }
}