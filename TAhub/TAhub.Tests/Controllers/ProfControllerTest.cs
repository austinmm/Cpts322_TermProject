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
    public class ProfControllerTest
    {
        [TestMethod]
        public void Index_NotLoggedIn()
        {
            ProfessorController controller = new ProfessorController();
            // Act
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;
            // Assert
            Assert.IsNotNull(result);
        }
        //[TestMethod]
        //public void Index_LoggedIn()
        //{
        //    ProfessorController controller = new ProfessorController();
        //    DB.Professor Prof_DB = DB.DBMethods.Prof_Get(500);//Fix
        //    Models.ProfessorModel Prof = DB.DBMethods.Prof_DBToModel(Prof_DB);
        //    Models.Cache.Set(Prof);
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
        //    ProfessorController controller = new ProfessorController();
        //    DB.Professor Prof_DB = DB.DBMethods.Prof_Get(500);//Fix
        //    Models.ProfessorModel Prof = DB.DBMethods.Prof_DBToModel(Prof_DB);
        //    Models.Cache.Set(Prof);
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
        //    ProfessorController controller = new ProfessorController();
        //    RedirectToRouteResult result = controller.AccountDetails("") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "Index" && result.RouteValues["controller"] == "Home");
        //    Models.ProfessorModel Prof = new Models.ProfessorModel();
        //    Prof.FirstName = "brandon";
        //    Prof.LastName = "somers";
        //    Prof.Login.Username = "brandon.somers@wsu.edu";
        //    Prof.Login.Password = "password1";
        //    Models.Cache.Set<Models.ProfessorModel>("User", Prof); //cache info 
        //    controller = new ProfessorController();
        //    ViewResult result2 = controller.AccountDetails("") as ViewResult;
        //    Assert.IsNotNull(result2.Model);
        //    Models.UserLogin.Reset();
        //}
        //[TestMethod]
        //public void DeleteAccount()
        //{
        //    //invalid deletion
        //    ProfessorController controller = new ProfessorController();
        //    RedirectToRouteResult result = controller.DeleteAccount("") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "Index" && result.RouteValues["controller"] == "Professor");

        //    //valid deletion
        //    Models.ProfessorModel Prof = new Models.ProfessorModel();
        //    Prof.FirstName = "John";
        //    Prof.LastName = "Somers";
        //    Prof.Login.Username = "john.somers@wsu.edu";
        //    Prof.Login.Password = "password1";
        //    Models.Cache.Set<Models.ProfessorModel>("User", Prof); //cache info 
        //    DB.Professor Prof_DB = DB.DBMethods.Prof_ModelToDB(Prof);
        //    Assert.IsNotNull(Prof_DB);
        //    string errors = DB.DBMethods.Insert(Prof_DB);
        //    Assert.IsTrue(String.IsNullOrWhiteSpace(errors));
        //    Assert.IsNotNull(DB.DBMethods.Prof_Get(500));//Fix
        //    controller = new ProfessorController();
        //    RedirectToRouteResult result2 = controller.DeleteAccount("john.somers@wsu.edu") as RedirectToRouteResult;
        //    Assert.IsNull(DB.DBMethods.Prof_Get(500));//Fix
        //    Assert.IsTrue(result2.RouteValues["action"] == "Index" && result2.RouteValues["controller"] == "Home");
        //    Models.UserLogin.Reset();
        //}
        //[TestMethod]
        //public void ChooseTAs()
        //{
        //    //invalid
        //    ProfessorController controller = new ProfessorController();
        //    RedirectToRouteResult result = controller.ChooseTAs("", "", "", "") as RedirectToRouteResult;
        //    Assert.IsTrue(result.RouteValues["action"] == "Index" && result.RouteValues["controller"] == "Home");

        //    //valid
        //    Models.ProfessorModel Prof = new Models.ProfessorModel();
        //    Prof.FirstName = "brandon";
        //    Prof.LastName = "somers";
        //    Prof.Login.Username = "brandon.somers@wsu.edu";
        //    Prof.Login.Password = "password1";
        //    Models.Cache.Set<Models.ProfessorModel>("User", Prof); //cache info 
        //    controller = new ProfessorController();
        //    ViewResult result2 = controller.ChooseTAs("", "", "", "") as ViewResult;
        //    Assert.IsNotNull(result2.Model);
        //    Models.UserLogin.Reset();
        //}
    }
}
