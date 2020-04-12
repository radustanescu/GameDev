using GameDev.BL.Utils;
using GameDev.DAL.Account;
using GameDev.DM.Account;
using GameDev.DM.Error;
using System;

namespace GameDev.BL.Account
{
    public class AccountService
    {
        private AccountRepository accountRepository = new AccountRepository();
        private SecurityService securityService = new SecurityService();

        public LoginModel CheckLogin(string userName, string password)
        {
            UserModel userModel = new UserModel();

            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[securityService.saltLengthLimit];

            userModel = accountRepository.GetUserByUserName(userName);
            if (userModel != null)
            {
                OldHASHValue = userModel.HASH;
                SALT = userModel.SALT;
            }

            bool isLogin = securityService.CompareHashValue(password, userName, OldHASHValue, SALT);

            LoginModel loginModel = new LoginModel
            {
                UserName = userName,
                IsLogin = true
            };
            if (!isLogin)
            {
                loginModel.IsLogin = false;
                return loginModel;
            }
            loginModel.UserID = userModel.UserID;

            return loginModel;
        }

        public bool Register(RegisterModel registerModel)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel = accountRepository.GetUserByUserName(registerModel.UserName);
                if (userModel != null)
                {
                    throw new HandledException("Username is taken!");
                }

                userModel = accountRepository.GetUserByEmail(registerModel.Email);
                if (userModel != null)
                {
                    throw new HandledException("Email is taken!");
                }

                byte[] SALT = securityService.Get_SALT();
                string HASH = securityService.Get_HASH_SHA512(registerModel.Password, registerModel.UserName, SALT);
                accountRepository.RegisterUser(registerModel.UserName, registerModel.Email, HASH, SALT);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}