using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAhub.Models;

namespace TAhub.Controllers
{
    public class HomeController : Controller
    {
        //Notice: All 'notification' paramters are for notifying users of internal errors/issues

        //Checks if someone is logged-in and reroutes them to their controllers Home Page
        public static UserType CheckLoginStatus()
        {
            if (Cache.GetUser<TAModel>() != null)
            {
                return UserType.TA;
            }
            else if (Cache.GetUser<ProfessorModel>() != null)
            {
                return UserType.Professor;
            }
            return UserType.Student;
        }

        //***** Error PAGE *****
        public ActionResult Error(string exception = "")
        {
            string message = !String.IsNullOrWhiteSpace(exception)? exception: "An Exception Has Occured. Please return to the last page you were safley on.";
            return View(model:message);
        }

        //***** HOME PAGE *****
        public ActionResult Index(string notification = "")
        {
            UserType user = CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            return View();
        }

        //***** ABOUT PAGE *****
        public ActionResult About(string notification = "")
        {
            UserType user = CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            //DB.DBTests.CreateTestData();
            return View();
        }

        //***** LOGIN PAGE *****
        [HttpGet]
        public ActionResult Login(string notification = "")
        {
            UserType user = CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            LoginInfo login = new LoginInfo() { Notifications = notification };
            return View(login);
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info)
        {
            switch (info.User)
            {
                case UserType.TA:
                    DB.TeachersAssistant TA_DB = DB.DBMethods.TA_HasAccount(info.Username, info.Password);
                    if(TA_DB != null)
                    {
                        TAModel TA_Model = DB.DBMethods.TA_DBToModel(TA_DB);
                        if (TA_Model != null)
                        {
                            Cache.Set(TA_Model);
                            return RedirectToAction(actionName: "Index", controllerName: "TA");
                        }

                    }
                    break;
                case UserType.Professor:
                    DB.Professor Prof_DB = DB.DBMethods.Prof_HasAccount(info.Username, info.Password);
                    if (Prof_DB != null)
                    {
                        ProfessorModel Prof_Model = DB.DBMethods.Prof_DBToModel(Prof_DB);
                        if (Prof_Model != null)
                        {
                            Cache.Set(Prof_Model);
                            return RedirectToAction(actionName: "Index", controllerName: "Professor");
                        }
                    }
                    break;
            }
            ModelState.Clear();
            return RedirectToAction(actionName: "Login", controllerName: "Home", routeValues: new { notification = "Invalid Username and/or Password" });
        }


        //***** SIGN OUT *****
        public ActionResult SignOut()
        {
            //Removes any cached data and sends the user to the default home page
            Cache.RemoveUser();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        //***** ACCOUNT DETAILS *****
        public ActionResult AccountDetails(UserType user)
        {
            switch (user)
            {
                //Goes to the account details page in the Professor Controller
                case UserType.Professor:
                    ProfessorModel Prof = Cache.GetUser<ProfessorModel>();
                    if(Prof == null) { break; }
                    return RedirectToAction(actionName: "AccountDetails", controllerName: "Professor");
                //Goes to the account details page in the TA Controller
                case UserType.TA:
                    TAModel TA = Cache.GetUser<TAModel>();
                    if (TA == null) { break; }
                    return RedirectToAction(actionName: "AccountDetails", controllerName: "TA");
                default: break;
            }
            //If there was an issue all data is reset by sending user to signout page
            return RedirectToAction(actionName: "SignOut", controllerName: "Home");
        }
    }
}