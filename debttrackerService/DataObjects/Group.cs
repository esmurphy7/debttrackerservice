using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace debttrackerService.DataObjects
{
    public class Group : EntityData
    {
        public int GroupId { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Debt> Debts { get; set; }
    }
}