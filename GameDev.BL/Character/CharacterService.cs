using GameDev.DAL.CharacterRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev.BL.Character
{
    public class CharacterService
    {
        private readonly CharacterRepository characterRepository = new CharacterRepository();

        public void CreateCharacterForUser(Guid userID, Guid classID)
        {
            characterRepository.CreateCharacterForUser(userID, classID);
        }
    }
}