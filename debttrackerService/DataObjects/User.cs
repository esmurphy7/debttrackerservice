using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace debttrackerService.DataObjects
{
    public class User : EntityData
    {
        public int UserId { get; set; }
        public Account Account { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}