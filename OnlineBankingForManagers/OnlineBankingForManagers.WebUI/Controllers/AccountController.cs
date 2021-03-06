﻿using System.Web.Mvc;
using OnlineBankingForManagers.Domain.Abstract;
using OnlineBankingForManagers.Domain.Components;
using OnlineBankingForManagers.WebUI.Models;
using OnlineBankingForManagers.Domain.Personages;
using OnlineBankingForManagers.WebUI.Infrastructure.Abstract;

namespace OnlineBankingForManagers.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserProvider _userProvider;
        private IAuthCookie authCookie;

        public AccountController(IUserProvider user, IAuthCookie cookie)
        {
            _userProvider = user;
            authCookie = cookie;
        }

        public ViewResult UnBlockedAccount( string login)
        {
            _userProvider.UnBlocked(login);
            return View(); 
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {

                DbResultType _result = _userProvider.Authentification(model.UserName, model.Password);

                if (_result == DbResultType.Executed)
                {
                    authCookie.AuthCookie(model.UserName, model.RememberMe);
              
                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect(Url.Action("List", "Client")); 
                    }                                      
                }
                else
                {
                    ModelState.AddModelError("", _result.ToString());
                    return View();
                }
            }
            else
            {
               
                return View();
            }
        }
        public ActionResult LogOff()
        {
            authCookie.AuthCookieOff();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            User user = new User();
            user.Login = model.UserName;
            user.Password = model.Password;
            user.Email= model.Email;
            user.Address = model.Address;
            var result = _userProvider.Edit(user);
            if (ModelState.IsValid)
            {
                if (result == DbResultType.Executed)
                {                  
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.ToString());
                }
            }
            return View(model);
        }
    }
}
