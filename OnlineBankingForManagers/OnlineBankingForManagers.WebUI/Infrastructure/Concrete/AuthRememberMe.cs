using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using OnlineBankingForManagers.WebUI.Infrastructure.Abstract;


namespace OnlineBankingForManagers.WebUI.Infrastructure.Concrete
{
    public class AuthRememberMe : IAuthCookie
    {
        public void AuthCookie(string login, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(login, rememberMe);
         
        }
        

        public void AuthCookieOff()
        {
            FormsAuthentication.SignOut();
        }

    }
    
}