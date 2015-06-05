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

namespace debttrackerService.Controllers
{
    // Allow traffic via AuthorizeLevel
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class CustomRegistrationController : ApiController
    {
        public ApiServices Services { get; set; }
        
        // POST api/CustomRegistration
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            // Validate email
            bool isValid;
            try 
            {
                var addr = new System.Net.Mail.MailAddress(registrationRequest.Email);
                isValid = true;
            }
            catch(Exception e)
            {
                isValid = false;
            }
            if (isValid == false)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid email address");
            }
            // Validate username
            else if (!Regex.IsMatch(registrationRequest.Username, "^[a-zA-Z0-9]{4,}$"))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid username (at least 4 chars, alphanumeric only)");
            }
            // Validate password
            else if (registrationRequest.Password.Length < 8)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid password (at least 8 chars required)");
            }

            debttrackerContext context = new debttrackerContext();
            Account account = context.Accounts.Where(a => a.Username == registrationRequest.Username).SingleOrDefault();
            if (account != null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "That username already exists.");
            }
            else
            {
                byte[] salt = CustomLoginProviderUtils.generateSalt();
                Account newAccount = new Account
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = registrationRequest.Username,
                    Salt = salt,
                    SaltedAndHashedPassword = CustomLoginProviderUtils.hash(registrationRequest.Password, salt)
                };
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.Created);
            }
        }
    }
}
