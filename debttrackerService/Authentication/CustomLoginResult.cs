using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.Authentication
{
    /// <summary>
    /// "This class represents a successfully login with the user ID and the authentication token. 
    /// Note that this class has the same shape as the MobileServiceUser class on the client, which makes it easier to hand the login response on a strongly-typed client."
    /// </summary>
    public class CustomLoginResult
    {
        public string UserId { get; set; }
        public string MobileServiceAuthenticationToken { get; set; }
    }
}
