using System;

namespace GameDev.DM.Account
{
    public class LoginModel
    {
        public bool IsLogin { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
        public Guid UserID { get; set; }
        public string ReturnURL { get; set; }
    }
}