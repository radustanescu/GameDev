using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev.DAL.ClassRepository
{
    public class ClassRepository
    {
        public List<DM.Character.Class> GetClasses()
        {
            List<DM.Character.Class> classes = new List<DM.Character.Class>();
            using (GameDevEntities db = new GameDevEntities())
            {
                classes = db.Classes.Select(el => new DM.Character.Class()
                {
                    ClassID = el.ClassID,
                    ClassName = el.ClassName,
                    Description = el.Description
                }).ToList();
            }

            return classes;
        }
    }
}