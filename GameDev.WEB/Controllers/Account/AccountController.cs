using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using GameDev.BL.Account;
using GameDev.DM.Account;
using GameDev.BL.Utils;
using System.Web.Security;
using System;
using GameDev.DM.Error;

namespace GameDev.WEB.Controllers.Account
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View("Login", loginModel);

            SecurityService securityService = new SecurityService();
            AccountService accountService = new AccountService();

            try
            {
                LoginModel response = accountService.CheckLogin(loginModel.UserName, loginModel.Password);

                if (response.IsLogin)
                {
                    SignInRemember(response.UserName, loginModel.RememberMe);

                    return RedirectToLocal(loginModel.ReturnURL);
                }
                else
                {
                    TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                    return View(loginModel);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return View("Register", registerModel);

            try
            {
                AccountService accountService = new AccountService();
                bool result = accountService.Register(registerModel);

                return Login();
            }
            catch (HandledException handledEx)
            {
                TempData["ErrorMSG"] = handledEx.Message;
                return View(registerModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                return RedirectToAction("Index", "Dashboard");
            }
            catch
            {
                throw;
            }
        }

        private void SignInRemember(string userName, bool isPersistent = false)
        {
            FormsAuthentication.SignOut();

            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }
    }
}