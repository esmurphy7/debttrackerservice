using Microsoft.WindowsAzure.Mobile.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.Authentication
{
    /// <summary>
    /// "This represents information about your user and will be made available to you on the backend via GetIdentitiesAsync. 
    /// If you are adding custom claims, make sure that they are captured in this object."
    /// </summary>
    public class CustomLoginProviderCredentials : ProviderCredentials
    {
        public CustomLoginProviderCredentials()
            : base(CustomLoginProvider.ProviderName)
        {

        }
    }
}
