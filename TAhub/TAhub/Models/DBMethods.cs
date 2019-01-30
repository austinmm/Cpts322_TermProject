using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TAhub.DB
{
    public static class DBMethods
    {
        #region Message Model/DB Conversions
        //***** Message DB *****
        public static Message Message_ModelToDB(Models.MessageModel Message_Model)
        {
            Message Message_DB = new Message();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Message_Model.Id = Message_DB.Id;
                    Message_DB.SenderName = Message_Model.SenderName;
                    Message_DB.SenderEmail = Message_Model.SenderEmail;
                    Message_DB.SenderType = Models.EnumValues.UserTypes[(int)Message_Model.SenderType];
                    Message_DB.Text = Message_Model.Text;
                    Message_DB.TeachersAssistantId = Message_Model.TA;
                }
                catch (Exception)
                {
                    Message_DB = null;
                }
            }
            return Message_DB;
        }

        //***** Message Model *****
        public static Models.MessageModel Message_DBToModel(Message Message_DB)
        {
            Models.MessageModel Message_Model = new Models.MessageModel();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Message_Model.Id = Message_DB.Id;
                    Message_Model.SenderName = Message_DB.SenderName;
                    Message_Model.SenderEmail = Message_DB.SenderEmail;
                    Message_Model.SenderType = (Models.UserType)Models.EnumValues.UserTypes.IndexOf(Message_DB.SenderType);
                    Message_Model.Text = Message_DB.Text;
                    Message_Model.TA = Message_DB.TeachersAssistantId;
                }
                catch (Exception)
                {
                    Message_Model = null;
                }
            }
            return Message_Model;
        }
#endregion

        #region TA Model/DB Conversions

        //***** TA DB *****
        public static TeachersAssistant TA_ModelToDB(Models.TAModel TA_Model)
        {
            TeachersAssistant TA_DB = new TeachersAssistant();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA_DB.StudentId = Int32.Parse(TA_Model.StudentId);
                    TA_DB.FirstName = TA_Model.FirstName;
                    TA_DB.LastName = TA_Model.LastName;
                    TA_DB.Email = TA_Model.Login.Username;
                    TA_DB.Password = TA_Model.Login.Password;
                    TA_DB.Credits = Int32.Parse(TA_Model.Credits);
                    TA_DB.GPA = TA_Model.GPA;
                    TA_DB.HasExperience = Models.EnumValues.Bools[(int)TA_Model.HasExperience];
                    TA_DB.Lab = TA_Model.Lab;
                    TA_DB.Gender = Models.EnumValues.Genders[(int)TA_Model.Gender];
                    TA_DB.Major = Models.EnumValues.Majors[(int)TA_Model.Major];
                    TA_DB.WeeklySchedule = TA_Model.WeeklySchedule;
                    int count = TA_Model.CoursePreferences != null ? TA_Model.CoursePreferences.Count : 0;
                    for(int i = 0; i < count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                TA_DB.Preference1 = TA_Model.CoursePreferences[i];
                                break;
                            case 1:
                                TA_DB.Preference2 = TA_Model.CoursePreferences[i];
                                break;
                            case 2:
                                TA_DB.Preference3 = TA_Model.CoursePreferences[i];
                                break;
                            default: break;
                        }
                    }
                    TA_DB.Course = TA_Model.CourseAssignment;
                    TA_DB.Messages = TA_Model.Messages;
                }
                catch(Exception)
                {
                    TA_DB = null;
                }
            }
            return TA_DB;
        }

        //***** TA Model *****
        public static Models.TAModel TA_DBToModel(DB.TeachersAssistant TA_DB)
        {
            Models.TAModel TA_Model = new Models.TAModel();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA_Model.Id = TA_DB.Id;
                    TA_Model.StudentId = TA_DB.StudentId.ToString();
                    TA_Model.FirstName = TA_DB.FirstName;
                    TA_Model.LastName = TA_DB.LastName;
                    TA_Model.Login.Username = TA_DB.Email;
                    TA_Model.Login.Password = TA_DB.Password;
                    TA_Model.Credits = TA_DB.Credits.ToString();
                    TA_Model.GPA = TA_DB.GPA;
                    TA_Model.HasExperience = (Models.Bools) Convert.ToInt32(TA_DB.HasExperience);
                    TA_Model.Lab = TA_DB.Lab;
                    TA_Model.Gender = (Models.GenderType) Models.EnumValues.Genders.IndexOf(TA_DB.Gender);
                    TA_Model.Major = (Models.MajorType)Models.EnumValues.Majors.IndexOf(TA_DB.Major);
                    TA_Model.CoursePreferences = new List<string>()
                        { TA_DB.Preference1, TA_DB.Preference2, TA_DB.Preference3};
                    try
                    {
                        TA_Model.WeeklySchedule = TA_DB.WeeklySchedule.ToList();
                        TA_Model.CourseAssignment = TA_DB.Course;
                        TA_Model.Messages = TA_DB.Messages.ToList();
                    }
                    catch (Exception) { }
                }
                catch (Exception)
                {
                    TA_Model = null;
                }
            }
            return TA_Model;
        }
#endregion

        #region Professor Model/DB Conversions

        //***** Professor DB *****
        public static Professor Prof_ModelToDB(Models.ProfessorModel Prof_Model)
        {
            Professor Prof_DB = new Professor();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Prof_DB.FirstName = Prof_Model.FirstName;
                    Prof_DB.LastName = Prof_Model.LastName;
                    Prof_DB.Email = Prof_Model.Login.Username;
                    Prof_DB.Password = Prof_Model.Login.Password;
                    if (Prof_Model.Courses != null && Prof_Model.Courses.Count > 0)
                    {
                        Prof_DB.Courses = Prof_Model.Courses;
                    }
                }
                catch (Exception)
                {
                    Prof_DB = null;
                }
            }
            return Prof_DB;
        }

        //***** Profesor Model *****
        public static Models.ProfessorModel Prof_DBToModel(Professor Prof_DB)
        {
            Models.ProfessorModel Prof_Model = new Models.ProfessorModel();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Prof_Model.Id = Prof_DB.Id;
                    Prof_Model.FirstName = Prof_DB.FirstName;
                    Prof_Model.LastName = Prof_DB.LastName;
                    Prof_Model.Login.Username = Prof_DB.Email;
                    Prof_Model.Login.Password = Prof_DB.Password;
                    try
                    {
                        Prof_Model.Courses = Prof_DB.Courses.ToList();
                    }
                    catch (Exception) { }
                }
                catch (Exception)
                {
                    Prof_Model = null;
                }
            }
            return Prof_Model;
        }
        #endregion

        #region Insert Entities into Database

        //***** Insert Message *****
        public static string Insert(Error Error_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    db.Errors.Add(Error_DB);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }

        //***** Insert Message *****
        public static string Insert(Message Message_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    db.Messages.Add(Message_DB);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }

        //***** Insert Schedule *****
        public static string Insert(Schedule Schedule_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    db.Schedules.Add(Schedule_DB);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }

        //***** Insert TA *****
        public static string Insert(TeachersAssistant TA_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    //Check for duplicates here
                    if (db.TAs.FirstOrDefault(x => x.Email == TA_DB.Email) == null)
                    {
                        db.TAs.Add(TA_DB);
                        db.SaveChanges();
                    }
                    else
                    {
                        errors += "Your email address has already been registered to another account. Do not try to fool us -_-\n";
                    }
                }
                catch (Exception ex)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }

        //***** Insert Professor *****
        public static string Insert(Professor Prof_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    //Check for duplicates here
                    if (db.Professors.FirstOrDefault(x => x.Email == Prof_DB.Email) == null)
                    {
                        db.Professors.Add(Prof_DB);
                        db.SaveChanges();
                    }
                    else
                    {
                        errors += "Your email address has already been registered to another account. Do not try to fool us -_-\n";
                    }
                }
                catch (Exception)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }

        //***** Insert Course *****
        public static string Insert(Course Course_DB)
        {
            string errors = String.Empty;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    //Check for duplicates here
                    Course course = db.Courses.FirstOrDefault(x => x.SLN == Course_DB.SLN);
                    if (course == null)
                    {
                        db.Courses.Add(Course_DB);
                        db.SaveChanges();
                    }
                    else
                    {
                        errors += $"Course '{Course_DB.SLN}' already exists in our system\n";
                    }
                }
                catch (Exception ex)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }
        #endregion

        #region Delete Entities from Database
        //***** Delete Message *****
        public static bool Delete_Message(int Id)
        {
            bool isSuccessful = true;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Message deleteMessage = db.Messages.FirstOrDefault(x => x.Id == Id);
                    db.Entry(deleteMessage).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;
        }

        //***** Delete Message *****
        public static bool Delete_Schedule(int Id)
        {
            bool isSuccessful = true;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Schedule deleteSchedule = db.Schedules.FirstOrDefault(x => x.Id == Id);
                    db.Entry(deleteSchedule).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;
        }

        //***** Delete Course *****
        public static bool Delete_Course(int Id)
        {
            bool isSuccessful = true;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Course deleteCourse = db.Courses.FirstOrDefault(x => x.Id == Id);
                    db.Entry(deleteCourse).Collection(s => s.TAs).Load(); // loads Courses TA's
                    db.Entry(deleteCourse).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;
        }

        //***** Delete TA by Id*****
        public static bool Delete_TA(int Id)
        {
            bool isSuccessful = true;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TeachersAssistant deleteTA = db.TAs.FirstOrDefault(x => x.Id == Id);
                    db.Entry(deleteTA).Collection(s => s.Messages).Load(); // loads TA's Messsages
                    //Removes TA's Messages from database
                    db.Messages.RemoveRange(deleteTA.Messages);
                    db.SaveChanges();
                    //Removes TA's WeeklySchedule from database
                    db.Schedules.RemoveRange(deleteTA.WeeklySchedule);
                    db.SaveChanges();
                    //Removes TA from database
                    db.Entry(deleteTA).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;
        }

        //***** Delete Professor *****
        public static bool Delete_Prof(int Id)
        {
            bool isSuccessful = true;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Professor deleteProf = db.Professors.FirstOrDefault(x => x.Id == Id);
                    db.Entry(deleteProf).Collection(s => s.Courses).Load(); // loads Professor's Courses
                    db.Entry(deleteProf).Collection(p => p.Courses).Query().Include(r => r.TAs).Load(); //Loads course's TAs
                    //Removes Professor's courses from database
                    db.Courses.RemoveRange(deleteProf.Courses);
                    db.SaveChanges();
                    //Removes Professor from database
                    db.Entry(deleteProf).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;
        }

        #endregion

        #region Get Entities from Database
        //***** Get TA From DB *****
        public static TeachersAssistant TA_Get(int Id)
        {
            TeachersAssistant TA;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA = db.TAs.FirstOrDefault(x => x.Id == Id);
                    db.Entry(TA).Reference(s => s.Course).Load(); // loads Course
                    db.Entry(TA).Collection(s => s.Messages).Load(); // loads Message collection
                    db.Entry(TA).Collection(s => s.WeeklySchedule).Load(); // loads Schedule collection
                }
                catch (Exception)
                {
                    TA = null;
                }
            }
            return TA;
        }

        //***** Get Prof From DB *****
        public static Professor Prof_Get(int Id)
        {
            Professor professor;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    professor = db.Professors.FirstOrDefault(x => x.Id == Id);
                    db.Entry(professor).Collection(p => p.Courses).Load(); //Loads Professor's Courses
                    db.Entry(professor).Collection(p => p.Courses).Query().Include(r => r.TAs).Load(); //Loads courses TAs
                }
                catch (Exception)
                {
                    professor = null;
                }
            }
            return professor;
        }

        //***** Get Course From DB *****
        public static Course Course_Get(int SLN)
        {
            Course course;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    course = db.Courses.FirstOrDefault(x => x.SLN == SLN);
                    db.Entry(course).Reference(s => s.Professor).Load(); // loads Course's Professor
                    db.Entry(course).Collection(s => s.TAs).Load(); // loads Course's TAs
                }
                catch (Exception)
                {
                    course = null;
                }
            }
            return course;
        }

        //***** Get TA's WeeklySchedule From DB *****
        public static List<Schedule> WeekelySchedule_Get(int TA_Id)
        {
            List<Schedule> weekelySchedule;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    weekelySchedule = db.Schedules.Where(x => x.TeachersAssistantId == TA_Id).ToList();
                }
                catch (Exception)
                {
                    weekelySchedule = null;
                }
            }
            return weekelySchedule;
        }

        //***** Get Schedule From DB *****
        public static Schedule Schedule_Get(int Id)
        {
            Schedule schedule;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    schedule = db.Schedules.FirstOrDefault(x => x.Id == Id);
                }
                catch (Exception)
                {
                    schedule = null;
                }
            }
            return schedule;
        }
        #endregion

        #region Check for existing Entities in Database

        //***** Check For TA Account *****
        public static TeachersAssistant TA_HasAccount(string email, string password)
        {
            TeachersAssistant TA;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA = db.TAs.FirstOrDefault(x => 
                        (x.Email == email 
                        && x.Password == password));
                    db.Entry(TA).Reference(s => s.Course).Load(); // loads Course
                    db.Entry(TA).Collection(s => s.Messages).Load(); // loads Message collection
                    db.Entry(TA).Collection(s => s.WeeklySchedule).Load(); // loads Schedule collection
                }
                catch (Exception)
                {
                    TA = null;
                }
            }
            return TA;
        }

        //***** Check For Prof Account *****
        public static Professor Prof_HasAccount(string email, string password)
        {
            Professor professor;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    professor = db.Professors.FirstOrDefault(x =>(x.Email == email && x.Password == password));
                    db.Entry(professor).Collection(p => p.Courses).Load(); //Loads Professor's Course
                    db.Entry(professor).Collection(p => p.Courses).Query().Include(r => r.TAs).Load(); //Loads courses TAs
                }
                catch (Exception)
                {
                    professor = null;
                }
            }
            return professor;
        }
        #endregion

        #region Update Entities in Database
        //*****Update TA Account Info *****
        public static TeachersAssistant Update(TeachersAssistant TA_DB)
        {
            TeachersAssistant TA_Original = null;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA_Original = db.TAs.FirstOrDefault(x => x.Id == TA_DB.Id);
                    db.Entry(TA_Original).Reference(s => s.Course).Load(); // loads Course
                    db.Entry(TA_Original).Collection(s => s.Messages).Load(); // loads Message collection
                    db.Entry(TA_Original).Collection(s => s.WeeklySchedule).Load(); // loads Schedule collection
                    if (TA_Original != null)
                    {
                        //First Name
                        if (!String.IsNullOrWhiteSpace(TA_DB.FirstName) 
                            && !String.Equals(TA_Original.FirstName,TA_DB.FirstName))
                        {
                            TA_Original.FirstName = TA_DB.FirstName;
                        }
                        //Last Name
                        if (!String.IsNullOrWhiteSpace(TA_DB.LastName)
                            && !String.Equals(TA_Original.LastName, TA_DB.LastName))
                        {
                            TA_Original.LastName = TA_DB.LastName;
                        }
                        //Email
                        if (!String.IsNullOrWhiteSpace(TA_DB.Email)
                            && !String.Equals(TA_Original.Email, TA_DB.Email))
                        {
                            TA_Original.Email = TA_DB.Email;
                        }
                        //Password
                        if (!String.IsNullOrWhiteSpace(TA_DB.Password)
                            && !String.Equals(TA_Original.Password, TA_DB.Password))
                        {
                            TA_Original.Password = TA_DB.Password;
                        }
                        //Gender
                        if (!String.IsNullOrWhiteSpace(TA_DB.Gender)
                            && !String.Equals(TA_Original.Gender, TA_DB.Gender))
                        {
                            TA_Original.Gender = TA_DB.Gender;
                        }
                        //Major
                        if (!String.IsNullOrWhiteSpace(TA_DB.Major)
                            && !String.Equals(TA_Original.Major, TA_DB.Major))
                        {
                            TA_Original.Major = TA_DB.Major;
                        }
                        //Lab
                        if (TA_Original.Lab != TA_DB.Lab)
                        {
                            TA_Original.Lab = TA_DB.Lab;
                        }
                        //GPA
                        if (!String.IsNullOrWhiteSpace(TA_DB.GPA)
                            && !String.Equals(TA_Original.GPA, TA_DB.GPA))
                        {
                            TA_Original.GPA = TA_DB.GPA;
                        }
                        //Credits
                        if (TA_Original.Credits != TA_DB.Credits
                            && TA_DB.Credits >= 0)
                        {
                            TA_Original.Credits = TA_DB.Credits;
                        }
                        db.Entry(TA_Original).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        TA_Original = null;
                    }
                }
                catch (Exception ex)
                {
                    TA_Original = null;
                }
            }
            return TA_Original;
        }

        //*****Update Professor Account Info *****
        public static Professor Update(Professor Prof_DB)
        {
            Professor Prof_Original = null;
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Prof_Original = db.Professors.FirstOrDefault(x => x.Id == Prof_DB.Id);
                    db.Entry(Prof_Original).Collection(p => p.Courses).Load(); //Loads Professor's Course
                    db.Entry(Prof_Original).Collection(p => p.Courses).Query().Include(r => r.TAs).Load(); //Loads courses TAs
                    if (Prof_Original != null)
                    {
                        //First Name
                        if (!String.IsNullOrWhiteSpace(Prof_DB.FirstName)
                            && !String.Equals(Prof_Original.FirstName, Prof_DB.FirstName))
                        {
                            Prof_Original.FirstName = Prof_DB.FirstName;
                        }
                        //Last Name
                        if (!String.IsNullOrWhiteSpace(Prof_DB.LastName)
                            && !String.Equals(Prof_Original.LastName, Prof_DB.LastName))
                        {
                            Prof_Original.LastName = Prof_DB.LastName;
                        }
                        //Email
                        if (!String.IsNullOrWhiteSpace(Prof_DB.Email)
                            && !String.Equals(Prof_Original.Email, Prof_DB.Email))
                        {
                            Prof_Original.Email = Prof_DB.Email;
                        }
                        //Password
                        if (!String.IsNullOrWhiteSpace(Prof_DB.Password)
                            && !String.Equals(Prof_Original.Password, Prof_DB.Password))
                        {
                            Prof_Original.Password = Prof_DB.Password;
                        }
                        db.Entry(Prof_Original).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        Prof_Original = null;
                    }
                }
                catch (Exception ex)
                {
                    Prof_Original = null;
                }
            }
            return Prof_Original;
        }

        //***** Update a TAs Course Information *****
        public static string TA_UpdateCourse(TeachersAssistant TA_DB, Nullable<int> CourseId)
        {
            string errors = "";
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    TA_DB.Lab = null;
                    TA_DB.CourseId = CourseId;
                    db.Entry(TA_DB).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    errors += "Unable to Process Request.\n";
                }
            }
            return errors;
        }
        #endregion

        //***** Get All TA From Course *****
        public static List<TeachersAssistant> TAs_GetFromCourse(int SLN)
        {
            List<TeachersAssistant> TAs = new List<TeachersAssistant>();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    Course course = db.Courses.FirstOrDefault(x => x.SLN == SLN);
                    TAs.AddRange(course.TAs);
                }
                catch (Exception)
                {
                    TAs = null;
                }
            }
            return TAs;
        }

        //***** Get All TA From DB that are not alreadyu assigned to another Course as a TA *****
        public static List<TeachersAssistant> TAs_GetUnassigned(string course)
        {

            List<TeachersAssistant> TAs = new List<TeachersAssistant>();
            using (TAhubContext db = new TAhubContext())
            {
                try
                {
                    course = course.Replace(" ", "").Replace("_", "").ToLower();
                    foreach (TeachersAssistant TA in db.TAs.Where(x => x.Course == null).ToList())
                    {
                        string preference1 = String.IsNullOrWhiteSpace(TA.Preference1) ? "" : TA.Preference1.Replace(" ", "").Replace("_", "").ToLower();
                        string preference2 = String.IsNullOrWhiteSpace(TA.Preference2) ? "" : TA.Preference2.Replace(" ", "").Replace("_", "").ToLower();
                        string preference3 = String.IsNullOrWhiteSpace(TA.Preference3) ? "" : TA.Preference3.Replace(" ", "").Replace("_", "").ToLower();
                        if(String.Equals(course, preference1) || String.Equals(course, preference2) || String.Equals(course, preference2))
                        {
                            TAs.Add(TA);
                        }
                    }

                }
                catch (Exception)
                {
                    TAs = null;
                }
            }
            return TAs;
        }
    }
}