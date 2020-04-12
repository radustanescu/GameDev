using GameDev.DM.Account;
using System;
using System.Linq;

namespace GameDev.DAL.Account
{
    public class AccountRepository
    {
        public UserModel GetUserByUserName(string userName)
        {
            UserModel userModel = null;
            using (GameDevEntities db = new GameDevEntities())
            {
                userModel = db.Users.Where(el => el.UserName == userName.Trim()).Select(el => new UserModel()
                {
                    UserID = el.UserID,
                    HASH = el.HASH,
                    SALT = el.SALT,
                    Email = el.Email,
                    UserName = el.UserName
                }).FirstOrDefault();
            }

            return userModel;
        }

        public UserModel GetUserByEmail(string email)
        {
            UserModel userModel = null;
            using (GameDevEntities db = new GameDevEntities())
            {
                userModel = db.Users.Where(el => el.Email == email.Trim()).Select(el => new UserModel()
                {
                    UserID = el.UserID,
                    HASH = el.HASH,
                    SALT = el.SALT,
                    Email = el.Email,
                    UserName = el.UserName
                }).FirstOrDefault();
            }

            return userModel;
        }

        public Guid? RegisterUser(string userName, string email, string HASH, byte[] SALT)
        {
            using (GameDevEntities db = new GameDevEntities())
            {
                try
                {
                    User newUser = new User()
                    {
                        Email = email,
                        HASH = HASH,
                        SALT = SALT,
                        UserName = userName,
                        UserID = Guid.NewGuid()
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return newUser.UserID;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}