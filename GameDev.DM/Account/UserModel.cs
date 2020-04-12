using System;

namespace GameDev.DM.Account
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HASH { get; set; }
        public byte[] SALT { get; set; }
    }
}