using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TAhub.Models;
using System.Net;
using System.Net.Mime;
using Newtonsoft.Json;

namespace TAhub.Controllers
{
    public class TAController : Controller
    {
        //Notice: All 'notification' paramters are for notifying users of internal errors/issues
        private static List<DB.Course> CoursePreferences { get; set; } = new List<DB.Course>();

        //***** HOME PAGE *****
        public ActionResult Index(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            TAModel user = null;
            if ((user = Cache.GetUser<TAModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            user.Notifications = notification;
            return View(user);
        }

        //***** CREATE ACCOUNT PAGE*****
        [HttpGet]
        public ActionResult Create(string notification = "")
        {
            CoursePreferences = new List<DB.Course>();
            UserType user = HomeController.CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            //Instatiates a TAModel object to be sent to the view
            TAModel TA = new TAModel() { Notifications = notification };
            return View(TA);
        }

        [HttpPost]
        public ActionResult Create(TAModel TA)
        {
            string message = "We were unable to Process you request at this time.";
            //Ensures that the TA parameter passed in is instatiated
            if (TA != null)
            {
                if (CoursePreferences != null && CoursePreferences.Count > 0)
                {
                    TA.CoursePreferences = CoursePreferences.Select(x => $"{x.Prefix} {x.CourseNumber}").ToList();
                }
                //Converts the TA paramter into a Database TA object
                DB.TeachersAssistant TA_DB = DB.DBMethods.TA_ModelToDB(TA);
                //Inserts the TA into the Database
                string errors = DB.DBMethods.Insert(TA_DB);
                TA = DB.DBMethods.TA_DBToModel(TA_DB);
                //If the TA was successfully inserted into the Database
                if (String.IsNullOrWhiteSpace(errors) &&  TA != null)
                {
                    //Caches and sets the static UserLogin classes data with the new TA account
                    Cache.Set(TA);
                    return RedirectToAction(actionName: "Index", controllerName: "TA");
                }
                message = errors;
            }
            CoursePreferences = new List<DB.Course>();
            //Redirects to the create TA page if there was an issue submitting the TA object into the Database
            return RedirectToAction(actionName: "Create", controllerName: "TA", routeValues: new { notification = message });
        }

        //***** ACCOUNT DETAILS PAGE*****
        [HttpGet]
        public ActionResult AccountDetails(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            TAModel user = null;
            if ((user = Cache.GetUser<TAModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            user.Notifications = notification;
            return View(user);
        }
        [HttpPost]
        public ActionResult AccountDetails(TAModel TA)
        {
            //Checks that the TA parameter is instatiated
            if (TA != null)
            {
                //Converts the TA model to a Database entity
                DB.TeachersAssistant TA_DB = DB.DBMethods.TA_ModelToDB(TA);
                TA_DB.Id = TA.Id;
                //Updates the TA Database Entity in the Database
                TA_DB = DB.DBMethods.Update(TA_DB);
                //Converts the updated TA_DB to TAModel object
                TA = DB.DBMethods.TA_DBToModel(TA_DB); 
                //If TA was successfully Updated into the Database
                if (TA != null)
                {
                    //Updates the existing cached TA Modal information with the new info
                    Cache.Set(TA);
                    return RedirectToAction(actionName: "AccountDetails", controllerName: "TA");
                }
            }
            return RedirectToAction(actionName: "AccountDetails", controllerName: "TA", routeValues: new { notification = "Unable to Update Account Info" });
        }

        //***** Schedule PAGE*****
        public ActionResult Schedule(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            TAModel user = null;
            if ((user = Cache.GetUser<TAModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            return View(user);
        }

        //***** DELETE ACCOUNT *****
        public ActionResult DeleteAccount(string Id)
        {
            if (Int32.TryParse(Id, out int id))
            {
                if (DB.DBMethods.Delete_TA(id))
                {
                    return RedirectToAction(actionName: "SignOut", controllerName: "Home");
                }
            }
            return RedirectToAction(actionName: "AccountDetails", controllerName: "TA", routeValues: new { notification = "Unable to Delete Account" });
        }

        //***** MESSAGES PAGE*****
        [HttpGet]
        public ActionResult Messages(string notification = "")
        {
            //Checks if a TAModal instance exist in the browser's cached data
            TAModel user = null;
            if ((user = Cache.GetUser<TAModel>()) == null)
            {
                return RedirectToAction(actionName: "SignOut", controllerName: "Home");
            }
            user.Notifications = notification;
            return View(user);
        }

        //***** SEND MESSAGES PAGE*****
        [HttpGet]
        public ActionResult Contact(string notification = "")
        {
            UserType user = HomeController.CheckLoginStatus();
            if (user != UserType.Student)
            {
                return RedirectToAction(actionName: "Index", controllerName: Models.EnumValues.UserTypes[(int)user]);
            }
            //Instatiates a MessageModal object to be sent to the view
            MessageModel message = new MessageModel() { Notifications = notification };
            return View(message);
        }
        [HttpPost]
        public ActionResult Contact(MessageModel message)
        {
            //Ensures that the message paramter is instatiated
            if(message != null)
            {
                //Converts the Message object to a Database object
                DB.Message DB_Message = DB.DBMethods.Message_ModelToDB(message);
                //Inserts the Message into the Database
                string errors = DB.DBMethods.Insert(DB_Message);
                //If the Message was successfully inserted into the database
                if (String.IsNullOrWhiteSpace(errors))
                {
                    return RedirectToAction(actionName: "Contact", controllerName: "TA", routeValues: new { notification = "Success, Your Message has been Sent!" });
                }
            }
            return RedirectToAction(actionName: "Contact", controllerName: "TA", routeValues: new { notification = "We are unable to Process you request at this time." });
        }

        //********** AJAX METHODS **********//
        //***** Delete Messages Ajax (made from Messages View)*****
        [HttpDelete]
        public JsonResult DeleteMessage(string Id)
        {
            //Attempts to convert the string representation of the Message Id to an int
            if (Int32.TryParse(Id, out int ID))
            {
                //Attempts to delete the message from the database
                if (DB.DBMethods.Delete_Message(ID))
                {
                    //Updates the cached TA to represent the updated message state
                    TAModel TA = Cache.GetUser<TAModel>();
                    DB.Message message = TA.Messages.FirstOrDefault(x => x.Id == ID);
                    TA.Messages.Remove(message);
                    Cache.Set(TA);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json("Message has been Successfully Deleted", MediaTypeNames.Text.Plain);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Delete Message", MediaTypeNames.Text.Plain);
        }

        //***** Get TA Options AJAX ***** (made from Send Message View Page)
        [HttpPost]
        public JsonResult GetTAOptions(string CourseSLN)
        {
            //Attempts to convert the string representation of the Cpurse SLN to an int
            if (Int32.TryParse(CourseSLN, out int SLN))
            {
                //Grabs all the TAs from the Database that are assigned to the course SLN
                List<DB.TeachersAssistant> TAs_DB = DB.DBMethods.TAs_GetFromCourse(SLN);
                //If TAs were found in the Database
                if (TAs_DB != null)
                {
                    //Converts all the TAs to a simple json class that only contains necessary TA and Course info
                    var TAs = TAs_DB.Select(x => new
                    {
                        Name = $"{x.FirstName} {x.LastName}",
                        Id = x.Id,
                        Course = $"{x.Course.Prefix} {x.Course.CourseNumber}",
                        CourseTitle = x.Course.CourseTitle
                    }).ToList();
                    var outlist = JsonConvert.SerializeObject(TAs,Formatting.None,
                        new JsonSerializerSettings(){
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        });
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(outlist, MediaTypeNames.Text.Plain);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Add Course Infomation", MediaTypeNames.Text.Plain);
        }

        //***** ADD COURSE Preference AJAX ***** (made from TA Create View Page)
        [HttpPost]
        public JsonResult AddCoursePreference(DB.Course course)
        {
            string message = "Failed to Add Course Infomation";
            if (course != null && course.SLN != default(int))
            {
                if (CoursePreferences.Count < 3)
                {
                    CoursePreferences.Add(course);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json("Success", MediaTypeNames.Text.Plain);
                }
                message = "You cannot add more than 3 courses.";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(message, MediaTypeNames.Text.Plain);
        }

        [HttpPost]
        public JsonResult AddSchedule(DB.Schedule schedule)
        {
            if(schedule != null && !String.IsNullOrWhiteSpace(schedule.Days) && !String.IsNullOrWhiteSpace(schedule.Time))
            {
                if (String.IsNullOrWhiteSpace(DB.DBMethods.Insert(schedule)))
                {
                    TAModel TA = Cache.GetUser<TAModel>();
                    if(TA.WeeklySchedule == null) { TA.WeeklySchedule = new List<DB.Schedule>(); }
                    TA.WeeklySchedule.Add(schedule);
                    Cache.Set(TA);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json("Success", MediaTypeNames.Text.Plain);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Add Course to your Schedule", MediaTypeNames.Text.Plain);
        }

        [HttpDelete]
        public JsonResult RemoveSchedule(string Id)
        {
            if (Int32.TryParse(Id, out int ScheduleId))
            {
                if (DB.DBMethods.Delete_Schedule(ScheduleId))
                {
                    TAModel TA = Cache.GetUser<TAModel>();
                    DB.Schedule schedule = TA.WeeklySchedule.FirstOrDefault(x => x.Id == ScheduleId);
                    TA.WeeklySchedule.Remove(schedule);
                    Cache.Set(TA);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json("Success", MediaTypeNames.Text.Plain);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Remove Course from your Schedule", MediaTypeNames.Text.Plain);
        }

        //***** REMOVE COURSE Preference AJAX ***** (made from TA Create View Page)
        [HttpDelete]
        public JsonResult RemoveCoursePreference(string SLN)
        {
            if (SLN != null && Int32.TryParse(SLN, out int SLN_Num))
            {
                DB.Course course = CoursePreferences.FirstOrDefault(x => x.SLN == SLN_Num);
                CoursePreferences.Remove(course);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Success", MediaTypeNames.Text.Plain);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to Remove Course Infomation", MediaTypeNames.Text.Plain);
        }
    }
}