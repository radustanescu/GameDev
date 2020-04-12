using GameDev.BL.Character;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameDev.WEB.Controllers.CharacterSelection
{
    public class ClassController : Controller
    {
        private readonly ClassService classService = new ClassService();

        public ActionResult Index()
        {
            List<DM.Character.Class> classes = new List<DM.Character.Class>();
            classes = classService.GetClasses();

            return View(classes);
        }
    }
}