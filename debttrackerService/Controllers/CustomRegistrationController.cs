using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using debttrackerService.Authentication;
using System.Text.RegularExpressions;
using debttrackerService.Models;
using debttrackerService.DataObjects;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;

namespace debttrackerService.Controllers
{
    // Allow traffic via AuthorizeLevel
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class CustomRegistrationController : ApiController
    {
        public ApiServices Services { get; set; }

        // POST api/CustomRegistration
        public RegistrationResponse Post(RegistrationRequest registrationRequest)
        {
            HttpStatusCode statusCode;
            string message = String.Empty;

            // Validate email
            bool isValid;
            try
            {
                var addr = new System.Net.Mail.MailAddress(registrationRequest.Email);
                isValid = true;
            }
            catch (Exception e)
            {
                isValid = false;
            }
            if (isValid == false)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Invalid email address";
            }
            // Validate username
            else if (!Regex.IsMatch(registrationRequest.Username, "^[a-zA-Z0-9]{4,}$"))
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Invalid username (at least 4 chars, alphanumeric only)";
            }
            // Validate password
            else if (registrationRequest.Password.Length < 8)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Invalid password (at least 8 chars required)";
            }

            debttrackerContext context = new debttrackerContext();
            Account account = context.Accounts.Where(a => a.Username == registrationRequest.Username).SingleOrDefault();
            if (account != null)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "That username already exists.";
            }
            else
            {
                byte[] salt = CustomLoginProviderUtils.generateSalt();
                Account newAccount = new Account
                {
                    Email = registrationRequest.Email,
                    Id = Guid.NewGuid().ToString(),
                    Username = registrationRequest.Username,
                    Salt = salt,
                    SaltedAndHashedPassword = CustomLoginProviderUtils.hash(registrationRequest.Password, salt)
                };
                context.Accounts.Add(newAccount);
                context.SaveChanges();

                statusCode = HttpStatusCode.Created;
                message = "Account Registered Successfully";
            }

            var response = new RegistrationResponse()
            {
                StatusCode = statusCode,
                Message = message
            };

            return response;
        }
    }
}
