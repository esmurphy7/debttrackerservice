using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.Authentication
{
    /// <summary>
    /// "This class represents an incoming sign-in attempt."
    /// </summary>
    public class LoginRequest
    {
        public string username{get;set;}
        public string password { get; set; }
    }
}
