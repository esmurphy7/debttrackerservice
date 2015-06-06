using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;
using debttrackerService.Authentication;
using debttrackerService.DataObjects;
using debttrackerService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;

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
                var addr = new MailAddress(registrationRequest.Email);
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
