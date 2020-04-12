using GameDev.DAL.ClassRepository;
using System.Collections.Generic;

namespace GameDev.BL.Character
{
    public class ClassService
    {
        public List<DM.Character.Class> GetClasses()
        {
            ClassRepository classRepository = new ClassRepository();
            return classRepository.GetClasses();
        }
    }
}