using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestDeamonApp
{
	class Program
	{
		static void Main(string[] args)
		{
            try
            {
                RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static async Task RunAsync()
        {
            AuthenticationConfig config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");
                        
            IPublicClientApplication app;

            app = PublicClientApplicationBuilder.Create(config.ClientId)
                .WithB2CAuthority(config.Authority)
                .Build();

            string[] scopes = new string[] { config.Scope };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenByUsernamePassword(scopes, config.NVEAccountEmail, config.SecretPassword).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Token acquired from {config.Authority} \n");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scope provided is not supported");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error acquiring token: {e.Message}");
                Console.ResetColor();
            }

            if (result != null)
            {
                var httpClient = new HttpClient();
                var apiCaller = new ProtectedApiCallHelper(httpClient);
                await apiCaller.CallWebApiAndProcessResultASync(config, $"{config.ApiBaseAddress}/{config.RegObsActionToCall}", result.AccessToken, Display);
            }
        }

        private static void Display(string result)
        {
            Console.WriteLine($"Web Api result: {result}");            
        }
    }
}
