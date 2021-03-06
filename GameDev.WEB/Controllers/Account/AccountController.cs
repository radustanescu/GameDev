﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using GameDev.BL.Account;
using GameDev.DM.Account;
using GameDev.BL.Utils;
using System.Web.Security;
using System;
using GameDev.DM.Error;
using System.Web;

namespace GameDev.WEB.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly SecurityService securityService = new SecurityService();
        private readonly AccountService accountService = new AccountService();

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

            try
            {
                LoginModel response = accountService.CheckLogin(loginModel.UserName, loginModel.Password);
                bool hasCharacter = accountService.HasCharacter(response.UserID);

                if (response.IsLogin)
                {
                    SignInRemember(response.UserName, loginModel.RememberMe);
                    HttpCookie cookie = new HttpCookie("LoggedInUserID")
                    {
                        Value = response.UserID.ToString()
                    };
                    HttpContext.Response.SetCookie(cookie);

                    if (hasCharacter == false)
                        loginModel.ReturnURL = "~/Class/Index";

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
                bool result = accountService.RegisterUser(registerModel);

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