using GameDev.DM.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev.DAL.CharacterRepository
{
    public class CharacterRepository
    {
        public void CreateCharacterForUser(Guid userID, Guid classID)
        {
            using (GameDevEntities db = new GameDevEntities())
            {
                if (db.Users.Where(el => el.UserID == userID).FirstOrDefault() == null)
                    throw new HandledException("The user is invalid");

                if (db.Classes.Where(el => el.ClassID == classID).FirstOrDefault() == null)
                    throw new HandledException("The class is invalid");

                if (db.Characters.Where(el => el.UserID == userID).FirstOrDefault() != null)
                    throw new HandledException("The user already has a class selected");

                Character newCharacter = new Character()
                {
                    ClassID = classID,
                    UserID = userID,
                    CharacterID = Guid.NewGuid(),
                    Level = 1,
                    Energy = 100,
                    Gold = 0,
                    GuildID = null,
                    Gems = 0
                };
                db.Characters.Add(newCharacter);

                User user = db.Users.Where(el => el.UserID == userID).FirstOrDefault();
                user.HasCharacter = true;

                db.SaveChanges();
            }
        }
    }
}