using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TAhub.Models
{
    public abstract class ModelClass
    {
        public int Id { get; set; }
        public string Notifications { get; set; }
    }
    public abstract class Account : ModelClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LoginInfo Login { get; set; }
    }

    public class TAModel : Account
    {
        public string StudentId { get; set; }
        public GenderType Gender { get; set; }
        public MajorType Major { get; set; }
        public string GPA { get; set; }
        public string Credits { get; set; }
        public Bools HasExperience { get; set; }
        public Nullable<int> Lab { get; set; }
        public List<string> CoursePreferences { get; set; } 
            = new List<string>() { String.Empty, String.Empty, String.Empty };
        public DB.Course CourseAssignment { get; set; }
        public List<DB.Message> Messages { get; set; }
        public List<DB.Schedule> WeeklySchedule { get; set; }
        public TAModel()
        {
            this.Login = new LoginInfo();

        }
        public TAModel(LoginInfo info)
        {
            this.Login = new LoginInfo(info);
        }
    }

    public class ProfessorModel : Account
    {
        public List<DB.Course> Courses { get; set; }

        public ProfessorModel() {
            this.Login = new LoginInfo();
            this.Courses = new List<DB.Course>();
        }
        public ProfessorModel(LoginInfo info)
        {
            this.Login = new LoginInfo(info);
            this.Courses = new List<DB.Course>();
        }
    }

    public class MessageModel : ModelClass
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public UserType SenderType { get; set; }
        public string Text { get; set; }
        public int TA { get; set; }
    }
}