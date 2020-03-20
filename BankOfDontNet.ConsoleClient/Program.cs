using IdentityModel.Client;
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
            var httpClient = new HttpClient();
            var discovery = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (discovery.IsError)
            {
                System.Console.WriteLine(discovery.Error);
                return;
            }
            var tokenResponse = await httpClient.RequestTokenAsync(new TokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Parameters =
                {
                    { "scope", "bankOfDotNetApi" },
                    { "grant_type", "client_credentials" }
                }
            });

            if (tokenResponse.IsError)
            {
                System.Console.WriteLine(tokenResponse.ErrorDescription);
                return;
            }
            System.Console.WriteLine(tokenResponse.Json);
        }
    }
}