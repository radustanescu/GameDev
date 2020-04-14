using GameDev.BL.Utils;
using GameDev.DAL.Account;
using GameDev.DM.Account;
using GameDev.DM.Error;
using System;

namespace GameDev.BL.Account
{
    public class AccountService
    {
        private readonly AccountRepository accountRepository = new AccountRepository();
        private readonly SecurityService securityService = new SecurityService();
        private readonly CharacterService characterService = new CharacterService();

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

        public bool RegisterUser(RegisterModel registerModel)
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
                Guid? userID = accountRepository.RegisterUser(registerModel.UserName, registerModel.Email, HASH, SALT);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool HasCharacter(Guid userID)
        {
            try
            {
                return accountRepository.HasCharacter(userID);
            }
            catch (HandledException ex)
            {
                throw ex;
            }
        }
    }
}