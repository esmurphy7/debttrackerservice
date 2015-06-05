using Microsoft.WindowsAzure.Mobile.Service;

namespace debttrackerService.DataObjects
{
    public class Account : EntityData
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
    }
}
