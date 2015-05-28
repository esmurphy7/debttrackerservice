using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.Authentication
{
    /// <summary>
    /// Collect and store other user information during registration here
    /// </summary>
    public class RegistrationRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
