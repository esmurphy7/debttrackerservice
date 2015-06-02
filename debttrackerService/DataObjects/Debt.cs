using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.DataObjects
{
    public class Debt : EntityData
    {        
        public User SourceUser { get; set; }
        public User TargetUser { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public Group Group { get; set; }
    }
}
