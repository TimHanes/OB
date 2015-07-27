using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using OnlineBankingForManagers.WebUI.Controllers;
using OnlineBankingForManagers.WebUI.Infrastructure.Abstract;
using OnlineBankingForManagers.WebUI.Models;

namespace OnlineBankingForManagers.NUnitTests
{
    [TestFixture]
    class AdminSecurityTests
    {
        [Test]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthCookie> mock = new Mock<IAuthCookie>();
            mock.Setup(m => m.ValidateUser("admin", "secret")).Returns(true);
            // Arrange - create the view model

            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "secret"
            };
            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);
            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");
            // Assert
            Assert.IsInstanceOfType(typeof(RedirectResult), result);
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [Test]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthCookie> mock = new Mock<IAuthCookie>();
            mock.Setup(m => m.ValidateUser("badUser", "badPass")).Returns(false);
            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };
            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);
            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");
            // Assert
            Assert.IsInstanceOfType(typeof(ViewResult), result);
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
