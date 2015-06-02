using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace debttrackerService.DataObjects
{
    public class Friend : EntityData
    {
        public int FriendId { get; set; }
        public User UserA { get; set; }
        public User UserB { get; set; }
    }
}
