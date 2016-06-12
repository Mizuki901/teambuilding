using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamBuilding.Controllers;
using TeamBuilding.Models;
using System.Web.Mvc;

namespace TeamBuilding.Tests
{
    [TestClass]
    public class LoginControllerTest
    {
        /// <summary>
        /// Test for ThemeCreate()
        /// </summary>
        [TestMethod]
        public void ThemeCreateTestAdmin()
        {
            LoginController loginController = new LoginController();
            UserViewModels userViewModels = new UserViewModels();
            userViewModels.Name = "Admin";

            RedirectToRouteResult result = loginController.ThemeCreate(userViewModels) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["action"], "Result");
        }

        /// <summary>
        /// Test for ThemeCreate()
        /// </summary>
        [TestMethod]
        public void ThemeCreateTestAnyName()
        {
            LoginController loginController = new LoginController();
            UserViewModels userViewModels = new UserViewModels();
            userViewModels.Name = "anyname";

            ActionResult result = loginController.ThemeCreate(userViewModels);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
