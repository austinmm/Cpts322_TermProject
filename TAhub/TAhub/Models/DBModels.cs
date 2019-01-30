using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAhub.DB
{
    public class TeachersAssistant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        public int Credits { get; set; }
        public string Preference1 { get; set; }
        public string Preference2 { get; set; }
        public string Preference3 { get; set; }
        [Required]
        public bool HasExperience { get; set; }
        public Nullable<int> Lab { get; set; }
        [ForeignKey("Course")]
        public Nullable<int> CourseId { get; set; }
        //Navigation property
        public Course Course { get; set; }
        //Navigation property
        public virtual ICollection<Message> Messages { get; set; }
        //Navigation property
        public virtual ICollection<Schedule> WeeklySchedule { get; set; }
    }

    public class Professor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //Navigation property
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SLN { get; set; }
        [Required]
        public string Prefix { get; set; }
        [Required]
        public int CourseNumber { get; set; }
        [Required]
        public int EnrollmentLimit { get; set; }
        [Required]
        public string CourseTitle { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Term { get; set; }
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        //Navigation property
        public Professor Professor { get; set; }
        //Navigation property
        public virtual ICollection<TeachersAssistant> TAs { get; set; }
    }

    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string SenderType { get; set; }
        [Required]
        public string Text { get; set; }
        [ForeignKey("TeachersAssistant")]
        public int TeachersAssistantId { get; set; }
        //Navigation property
        public TeachersAssistant TeachersAssistant { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Days { get; set; }
        [ForeignKey("TeachersAssistant")]
        public int TeachersAssistantId { get; set; }
        //Navigation property
        public TeachersAssistant TeachersAssistant { get; set; }
    }

    public class Error
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public bool HasInnerException { get; set; }
        //Optional
        public string InnerExceptionMessage { get; set; }
        public Nullable<int> LineNumber { get; set; }
        public string FileName { get; set; }
        public string NameSpace { get; set; }
        public string FilePath { get; set; }
    }


    public class TAhubContext : DbContext
    {
        public TAhubContext() : base("TAhubDB")
        {
            //Database.SetInitializer<TAhubContext>(new CreateDatabaseIfNotExists<TAhubContext>());
            Database.SetInitializer<TAhubContext>(new DropCreateDatabaseIfModelChanges<TAhubContext>());
            //Database.SetInitializer<TAhubContext>(new DropCreateDatabaseAlways<TAhubContext>());
            //Database.SetInitializer<TAhubContext>(new SchoolDBInitializer());
            /*Disable initializer
            Database.SetInitializer<SchoolDBContext>(null);*/
        }

        public DbSet<TeachersAssistant> TAs { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Error> Errors { get; set; }
    }
}