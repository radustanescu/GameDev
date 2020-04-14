using GameDev.BL.Character;
using GameDev.BL.Utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameDev.WEB.Controllers.CharacterSelection
{
    public class ClassController : Controller
    {
        private readonly ClassService classService = new ClassService();
        private readonly DataConverter dataConverter = new DataConverter();
        private readonly CharacterService characterService = new CharacterService();

        public ActionResult Index()
        {
            List<DM.Character.Class> classes = new List<DM.Character.Class>();
            classes = classService.GetClasses();

            return View(classes);
        }

        [HttpPost]
        public ActionResult CreateCharacter(Guid classID)
        {
            try
            {
                Guid userID = Guid.Parse(Request.Cookies.Get("LoggedInUserID").Value);
                characterService.CreateCharacterForUser(userID, classID);

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}