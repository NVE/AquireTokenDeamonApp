using Microsoft.Extensions.Configuration;
using System.IO;
using System.Security;

namespace TestDeamonApp
{
	/// <summary>
	/// Description of the configuration of an AzureAD public client application (desktop/mobile application). This should
	/// match the application registration done in the Azure portal
	/// </summary>
	public class AuthenticationConfig
    {
        /// <summary>
        /// Guid used by the application to uniquely identify itself to Azure AD
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// URL of the authority
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Web Api base URL
        /// </summary>
        public string ApiBaseAddress { get; set; }

        /// <summary>
        /// Web Api scope. With client credentials flows, the scopes is ALWAYS of the shape "resource/.default"
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Regobs App token, a GUID, needed to be able to call the API
        /// </summary>
        public string RegObsApptoken { get; set; }

        /// <summary>
        /// Example regObs API endpoint to call. e.g Account/GetObserver, will get info about the logged in observer
        /// </summary>
        public string RegObsActionToCall { get; set; }

        /// <summary>
        /// NVE-Account login email address
        /// </summary>
        public string NVEAccountEmail { get; set; }

        /// <summary>
        /// NVE-Account password
        /// </summary>
        public string NVEAccountPassword { get; set; }


        public SecureString SecretPassword
        {
            get
            {
                var passwordText = new SecureString();
                foreach (var c in NVEAccountPassword)
                {
                    passwordText.AppendChar(c);
                }
                return passwordText;
            }
        }

        /// <summary>
        /// Reads the configuration from a json file
        /// </summary>
        /// <param name="path">Path to the configuration json file</param>
        /// <returns>AuthenticationConfig read from the json file</returns>
        public static AuthenticationConfig ReadFromJsonFile(string path)
        {
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path);

            Configuration = builder.Build();
            return Configuration.Get<AuthenticationConfig>();
        }
    }
}
