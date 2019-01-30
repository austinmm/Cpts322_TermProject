using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAhub;
using TAhub.Controllers;

namespace TAhub.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login() 
        {
            //invalid prof login
            Models.LoginInfo login = new Models.LoginInfo();
            login.User = Models.UserType.Professor;
            login.Username = "brandon.somers@wsu.edu";
            login.Password = "password1";
            HomeController controller = new HomeController();
            RedirectToRouteResult result = controller.Login(login) as RedirectToRouteResult;
            Assert.IsTrue(result.RouteValues["action"] == "Login" && result.RouteValues["controller"] == "Home");

            //valid prof login
            login = new Models.LoginInfo();
            login.User = Models.UserType.Professor;
            login.Username = "b@wsu.edu";
            login.Password = "password1";
            controller = new HomeController();
            RedirectToRouteResult result2 = controller.Login(login) as RedirectToRouteResult;
            Assert.IsTrue(result2.RouteValues["action"] == "Index" && result2.RouteValues["controller"] == "Professor");

            //invalid TA login
            login = new Models.LoginInfo();
            login.User = Models.UserType.TA;
            login.Username = "rtf.somers@wsu.edu";
            login.Password = "password1";
            controller = new HomeController();
            RedirectToRouteResult result3 = controller.Login(login) as RedirectToRouteResult;
            Assert.IsTrue(result3.RouteValues["action"] == "Login" && result3.RouteValues["controller"] == "Home");

            //valid TA login
            login = new Models.LoginInfo();
            login.User = Models.UserType.TA;
            login.Username = "brandon.somers@wsu.edu";
            login.Password = "password1";
            controller = new HomeController();
            RedirectToRouteResult result4 = controller.Login(login) as RedirectToRouteResult;
            Assert.IsTrue(result4.RouteValues["action"] == "Index" && result4.RouteValues["controller"] == "TA");
        }

        [TestMethod]
        public void SignOut()
        {
            HomeController controller = new HomeController();
            RedirectToRouteResult result = controller.SignOut() as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void AccountDetails()
        //{
        //    //TA
        //    Models.TAModel TA1 = new Models.TAModel();
        //    TA1.FirstName = "brandon";
        //    TA1.LastName = "somers";
        //    TA1.Login.Username = "brandon.somers@wsu.edu";
        //    TA1.Login.Password = "password1";
        //    Models.Cache.Set<Models.TAModel>("User", TA1); //cache info
        //    HomeController controller = new HomeController();
        //    RedirectToRouteResult result = controller.AccountDetails(Models.UserType.TA) as RedirectToRouteResult;
        //    Assert.IsNotNull(result);
        //    Models.UserLogin.Reset();

        //    //Prof 
        //    Models.ProfessorModel prof = new Models.ProfessorModel();
        //    prof.FirstName = "b";
        //    prof.LastName = "s";
        //    prof.Login.Username = "b@wsu.edu";
        //    prof.Login.Password = "password1";
        //    Models.Cache.Set<Models.ProfessorModel>("User", prof); //cache info
        //    controller = new HomeController();
        //    RedirectToRouteResult result2 = controller.AccountDetails(Models.UserType.Professor) as RedirectToRouteResult;
        //    Assert.IsNotNull(result2);
        //    Models.UserLogin.Reset();
        //}
    }
}
