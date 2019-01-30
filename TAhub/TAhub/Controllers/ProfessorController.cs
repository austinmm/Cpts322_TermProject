using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using TAhub.Models;

namespace TAhub.Controllers
{
    public class ProfessorController : Controller
    {
        private static List<DB.Course> Courses { get; set; } = new List<DB.Course>();
        //Used Exclusivley for the ChooseTAs and ViewTAs Method
        public class TAOptions
        {
            public int CourseId { get; set; }
            public string CourseName { get; set; }
            public List<DB.TeachersAssistant> TAs { get; set; } = new List<DB.TeachersAssistant>();
            public string Notifications { get; set; }
        }

        //***** HOME PAGE *****
        public ActionResult Index(string notification = "")
        {
            //Checks that a valid Professor account is cached in the browser's memory
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            return View(user);
        }

        //***** CREATE ACCOUNT PAGE *****
        [HttpGet]
        public ActionResult Create(string notification = "")
        {
            UserType user = HomeController.CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            //instatiates a list of course modals to contain all the courses a professor making a new account adds
            Courses = new List<DB.Course>();
            ProfessorModel professor = new ProfessorModel() { Notifications = notification };
            return View(professor);
        }

        [HttpPost]
        public ActionResult Create(ProfessorModel professor)
        {
            //Ensures that the professor object is instatiated
            string message = "We were unable to Process you request at this time.";
            if (professor != null)
            {
                //Adds the list of courses to the professor object
                professor.Courses.AddRange(Courses);
                //Converts the prof object to a Database object
                DB.Professor Prof_DB = DB.DBMethods.Prof_ModelToDB(professor);
                //Inserts the new professor into the Database
                string errors = DB.DBMethods.Insert(Prof_DB);
                professor = DB.DBMethods.Prof_DBToModel(Prof_DB);
                //If successfully added professor into the Database
                if (String.IsNullOrWhiteSpace(errors) && professor != null)
                {
                    Cache.Set(professor);
                    return RedirectToAction(actionName: "Index", controllerName: "Professor");
                }
                message = errors;
            }
            return RedirectToAction(actionName: "Create", controllerName: "Professor", routeValues: new { notification = message });
        }

        //***** ViewTAs *****
        public ActionResult ViewTAs(string Id, string prefix, string number, string notification = "")
        {
            //Ensures the Professor is still logged in
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            if (Int32.TryParse(Id, out int id)
                && !string.IsNullOrWhiteSpace(prefix) && !string.IsNullOrWhiteSpace(number))
            {
                //Creates a generic object containing all important info for 'ChooseTAs' View
                TAOptions options = new TAOptions() { CourseId = id, CourseName = $"{prefix} {number}", Notifications = notification };
                DB.Course course = user.Courses != null ? user.Courses.FirstOrDefault(x => x.Id == id):null;
                if (course != null && course.TAs != null && course.TAs.Count > 0)
                {
                    options.TAs = course.TAs.ToList();
                }
                user.Notifications = notification;
                return View(options);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Professor");
        }

        //***** ChooseTAs *****
        public ActionResult ChooseTAs(string Id, string prefix, string number, string notification = "")
        {
            //Ensures the Professor is still logged in
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            if(Int32.TryParse(Id, out int id) 
                && !string.IsNullOrWhiteSpace(prefix) && !string.IsNullOrWhiteSpace(number))
            {
                //Creates a generic object containing all important info for 'ChooseTAs' View
                TAOptions options = new TAOptions() { CourseId = id, CourseName = $"{prefix} {number}", Notifications = notification };
                options.TAs = DB.DBMethods.TAs_GetUnassigned($"{prefix} {number}");
                user.Notifications = notification;
                return View(options);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Professor");
        }

        //***** ACCOUNT DETAILS PAGE*****
        [HttpGet]
        public ActionResult AccountDetails(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            user.Notifications = notification;
            return View(user);
        }
        [HttpPost]
        public ActionResult AccountDetails(ProfessorModel Prof)
        {
            //Checks that the TA parameter is instatiated
            if (Prof != null)
            {
                //Converts the TA model to a Database entity
                DB.Professor Prof_DB = DB.DBMethods.Prof_ModelToDB(Prof);
                Prof_DB.Id = Prof.Id;
                //Updates the TA Database Entity in the Database
                Prof_DB = DB.DBMethods.Update(Prof_DB);
                //Converts the updated TA_DB to TAModel object
                Prof = DB.DBMethods.Prof_DBToModel(Prof_DB);
                //If TA was successfully Updated into the Database
                if (Prof != null)
                {
                    //Updates the existing cached TA Modal information with the new info
                    Cache.Set(Prof);
                    return RedirectToAction(actionName: "AccountDetails", controllerName: "Professor");
                }
            }
            return RedirectToAction(actionName: "AccountDetails", controllerName: "Professor", routeValues: new { notification = "Unable to Update Account Info" });
        }

        //***** Edit Courses PAGE*****
        public ActionResult EditCourses(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            user.Notifications = notification;
            return View(user);
        }

        //***** DELETE ACCOUNT *****
        public ActionResult DeleteAccount(string Id)
        {
            if (Int32.TryParse(Id, out int id))
            {
                if (DB.DBMethods.Delete_Prof(id))
                {
                    return RedirectToAction(actionName: "SignOut", controllerName: "Home");
                }
            }
            return RedirectToAction(actionName: "AccountDetails", controllerName: "Professor", routeValues: new { notification = "Unable to Delete Account" });
        }

        //********** AJAX METHODS **********//
        //***** Message TAs AJAX ***** (made from Professor ViewTAs View Page)
        public JsonResult MessageTAs(string TAs, string text)
        {
            string response = "Unable to message TAs sepcified.";
            ProfessorModel user = Cache.GetUser<ProfessorModel>();
            if(user == null) { response = "Issue obtaining account details. Please sign out and back in again."; }
            List<string> Ids = TAs.Split(',').ToList();
            if (Ids != null && Ids.Count > 0 && !String.IsNullOrWhiteSpace(text) && user != null)
            {
                foreach (string TA_Id in Ids)
                {
                    if (Int32.TryParse(TA_Id, out int Id) )
                    {
                        DB.Message message = new DB.Message();
                        message.TeachersAssistantId = Id;
                        message.SenderType = "Professor";
                        message.SenderName = $"{user.FirstName} {user.LastName}";
                        message.SenderEmail = user.Login.Username;
                        message.Text = text;
                        string errors = DB.DBMethods.Insert(message);
                    }
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Success", MediaTypeNames.Text.Plain);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(response, MediaTypeNames.Text.Plain);
        }
        //***** ADD TAs to Course AJAX ***** (made from Professor ChooseTAs View Page)
        [HttpPost]
        public JsonResult AddTAToCourse(string TA, string Course)
        {
            if (Int32.TryParse(TA, out int TA_Id) && Int32.TryParse(Course, out int Course_Id))
            {
                DB.TeachersAssistant TA_DB = DB.DBMethods.TA_Get(TA_Id);
                if (String.IsNullOrWhiteSpace(DB.DBMethods.TA_UpdateCourse(TA_DB, Course_Id)))
                {
                    ProfessorModel Prof = Cache.GetUser<ProfessorModel>();
                    DB.Professor Prof_DB = DB.DBMethods.Prof_Get(Prof.Id);
                    Prof = DB.DBMethods.Prof_DBToModel(Prof_DB);
                    if (Prof != null)
                    {
                        Cache.Set(Prof);
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json("Success", MediaTypeNames.Text.Plain);
                    }
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Unable to add TA to your course.", MediaTypeNames.Text.Plain);
        }

        //***** ADD COURSE AJAX ***** (made from Professor Create View Page)
        [HttpPost]
        public JsonResult AddCourse(DB.Course course)
        {
            string message = "Failed to Add Course";
            if (course != null && course.SLN != 0 && Int32.TryParse(course.Term, out int term))
            {
                if (DB.DBMethods.Course_Get(course.SLN) == null)
                {
                    course.Term = EnumValues.Terms[term-1];
                    ProfessorModel user = null;
                    if ((user = Cache.GetUser<ProfessorModel>()) != null)
                    {
                        if (!String.IsNullOrWhiteSpace(DB.DBMethods.Insert(course)))
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json("Failed to Add Course.", MediaTypeNames.Text.Plain);
                        }
                        user.Courses.Add(course);
                        Cache.Set(user);
                    }
                    else
                    {
                        Courses.Add(course);
                    }
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json("Success", MediaTypeNames.Text.Plain);
                }
                message = "That Course has already been assigned to another professor in our Database.\nPlease try another course.";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(message, MediaTypeNames.Text.Plain);
        }

        //***** REMOVE COURSE AJAX ***** (made from Professor Create View Page)
        [HttpDelete]
        public JsonResult RemoveCourse(string SLN)
        {
            ProfessorModel user = null;
            if ((user = Cache.GetUser<ProfessorModel>()) != null)
            {
                if (SLN != null && Int32.TryParse(SLN, out int Id))
                {
                    if (DB.DBMethods.Delete_Course(Id))
                    {
                        DB.Course course = user.Courses.FirstOrDefault(x => x.Id == Id);
                        user.Courses.Remove(course);
                        Cache.Set(user);
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json("Success", MediaTypeNames.Text.Plain);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Failed to Remove Course.", MediaTypeNames.Text.Plain);
                }
            }
            else if (SLN != null && Int32.TryParse(SLN, out int SLN_Num))
            {
                DB.Course course = Courses.FirstOrDefault(x => x.SLN == SLN_Num);
                Courses.Remove(course);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Success", MediaTypeNames.Text.Plain);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Remove Course Infomation", MediaTypeNames.Text.Plain);
        }
        public JsonResult GetTASchedule(string TA)
        {
            string message = "Failed to Retrieve TA's Weekly Schedule";
            if (Int32.TryParse(TA, out int TA_ID))
            {
                List<DB.Schedule> WeeklySchedule = DB.DBMethods.WeekelySchedule_Get(TA_ID);
                if (WeeklySchedule != null)
                {
                    if (WeeklySchedule.Count != 0)
                    {
                        //Converts all the TAs to a simple json class that only contains necessary TA and Course info
                        var outlist = JsonConvert.SerializeObject(WeeklySchedule, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(outlist, MediaTypeNames.Text.Plain);
                    }
                    message = "Student has not set their schedule yet";
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(message, MediaTypeNames.Text.Plain);
        }
    }
}