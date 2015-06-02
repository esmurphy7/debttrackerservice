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
        public User UserA { get; set; }
        public User UserB { get; set; }
        public FriendStatus Status { get; set; }
    }

    public sealed class FriendStatus
    {
        public static readonly FriendStatus Accepted = new FriendStatus("accepted");
        public static readonly FriendStatus Pending = new FriendStatus("pending");
        public static readonly FriendStatus Declined = new FriendStatus("declined");

        public string Value { get; private set; }

        private FriendStatus(string status)
        {
            Value = status;
        }
    }
}
