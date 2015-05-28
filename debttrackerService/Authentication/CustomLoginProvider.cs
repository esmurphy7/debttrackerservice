using Microsoft.WindowsAzure.Mobile.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.Authentication
{
    public class CustomLoginProvider : LoginProvider
    {
        public const string ProviderName = "custom";

        public override string Name
        {
            get { return ProviderName; }
        }

        public CustomLoginProvider(IServiceTokenHandler tokenHandler)
            : base(tokenHandler)
        {
            this.TokenLifetime = new TimeSpan(30, 0, 0, 0);
        }

        public override void ConfigureMiddleware(Owin.IAppBuilder appBuilder, Microsoft.WindowsAzure.Mobile.Service.ServiceSettingsDictionary settings)
        {
            // Not Applicable - used for federated identity flows
            return;
        }

        /// <summary>
        /// "This method translates a ClaimsIdentity into a ProviderCredentials object that is used in the authentication token issuance phase. 
        /// You will again want to capture any additional claims in this method."
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public override ProviderCredentials CreateCredentials(System.Security.Claims.ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            string username = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            CustomLoginProviderCredentials credentials = new CustomLoginProviderCredentials
            {
                UserId = this.TokenHandler.CreateUserId(this.Name, username)
            };

            return credentials;
        }

        /// <summary>
        /// "This method will allow the backend to deserialize user information from an incoming authentication token."
        /// </summary>
        /// <param name="serialized"></param>
        /// <returns></returns>
        public override ProviderCredentials ParseCredentials(Newtonsoft.Json.Linq.JObject serialized)
        {
            if (serialized == null) { throw new ArgumentNullException("serialized"); }

            return serialized.ToObject<CustomLoginProviderCredentials>();

        }
    }
}
