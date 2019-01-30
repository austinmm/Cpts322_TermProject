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
    public class TAControllerTest
    {
        [TestMethod]
        public void Index_NotLoggedIn()
        {
            TAController controller = new TAController();
            // Act
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void Index_LoggedIn()
        //{
        //    TAController controller = new TAController();
        //    DB.TeachersAssistant TA_DB = DB.DBMethods.TA_Get(507);
        //    Models.TAModel TA = DB.DBMethods.TA_DBToModel(TA_DB);
        //    Models.Cache.Set(TA);
        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;
        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(result.Model);
        //    Models.UserLogin.Reset();
        //}

        //[TestMethod]
        //public void Create()
        //{
        //    TAController controller = new TAController();
        //    DB.TeachersAssistant TA_DB = DB.DBMethods.TA_Get(507);
        //    Models.TAModel TA = DB.DBMethods.TA_DBToModel(TA_DB);
        //    Models.Cache.Set(TA);
        //    // Act
        //    ViewResult result = controller.Create("") as ViewResult;
        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(result.Model);
        //    Models.UserLogin.Reset();
        //}

        //[TestMethod]
        //public void AccountDetails()
        //{        
        //    TAController controller = new TAController();
        //    RedirectToRouteResult result = controller.AccountDetails("") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "Index" && result.RouteValues["controller"] == "Home");
        //    Models.TAModel TA1 = new Models.TAModel();
        //    TA1.FirstName = "brandon";
        //    TA1.LastName = "somers";
        //    TA1.Login.Username = "brandon.somers@wsu.edu";
        //    TA1.Login.Password = "password1";
        //    Models.Cache.Set<Models.TAModel>("User", TA1); //cache info 
        //    controller = new TAController();
        //    ViewResult result2 = controller.AccountDetails("") as ViewResult;
        //    Assert.IsNotNull(result2.Model);
        //    Models.UserLogin.Reset();
        //}

        //[TestMethod]
        //public void Messages()
        //{
        //    //invalid
        //    TAController controller = new TAController();
        //    RedirectToRouteResult result = controller.Messages("") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "Index" && result.RouteValues["controller"] == "Home");

        //    //valid 
        //    Models.TAModel TA1 = new Models.TAModel();
        //    TA1.FirstName = "brandon";
        //    TA1.LastName = "somers";
        //    TA1.Login.Username = "brandon.somers@wsu.edu";
        //    TA1.Login.Password = "password1";
        //    Models.Cache.Set<Models.TAModel>("User", TA1); //cache info 
        //    controller = new TAController();
        //    ViewResult result2 = controller.Messages("") as ViewResult;
        //    Assert.IsNotNull(result2.Model);
        //    Models.UserLogin.Reset();
        //}

        [TestMethod]
        public void Contact()
        {
            TAController controller = new TAController();
            ViewResult result = controller.Contact("") as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        //[TestMethod]
        //public void DeleteAccount()
        //{
        //    //invalid deletion
        //    TAController controller = new TAController();
        //    RedirectToRouteResult result = controller.DeleteAccount("11511537") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "AccountDetails" && result.RouteValues["controller"] == "TA");

        //    //valid deletion
        //    Models.TAModel TA1 = new Models.TAModel();
        //    TA1.StudentId = "78507525";
        //    TA1.FirstName = "John";
        //    TA1.LastName = "Somers";
        //    TA1.Login.Username = "john.somers@wsu.edu";
        //    TA1.Login.Password = "password1";
        //    TA1.GPA = "3.2";
        //    TA1.Credits = "01";
        //    Models.Cache.Set<Models.TAModel>("User", TA1); //cache info 
        //    DB.TeachersAssistant TA_DB = DB.DBMethods.TA_ModelToDB(TA1);
        //    Assert.IsNotNull(TA_DB);
        //    string errors = DB.DBMethods.Insert(TA_DB);
        //    Assert.IsTrue(String.IsNullOrWhiteSpace(errors));
        //    Assert.IsNotNull(DB.DBMethods.TA_Get(TA_DB.Id));
        //    controller = new TAController();
        //    RedirectToRouteResult result2 = controller.DeleteAccount("78507525") as RedirectToRouteResult;
        //    Assert.IsNull(DB.DBMethods.TA_Get(TA_DB.Id));
        //    Assert.IsTrue(result2.RouteValues["action"] == "Index" && result2.RouteValues["controller"] == "Home");
        //    Models.UserLogin.Reset();
        //}
    }
}
