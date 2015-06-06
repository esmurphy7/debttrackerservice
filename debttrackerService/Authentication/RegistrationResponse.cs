using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace debttrackerService.Authentication
{
    public class RegistrationResponse
    {
        [JsonProperty(PropertyName = "StatusCode", Required = Required.Always)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(PropertyName = "Message", Required = Required.Always)]
        public string Message { get; set; }
    }
}
