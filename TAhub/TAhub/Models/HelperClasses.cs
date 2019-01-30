using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAhub.Models
{
    public static class EnumValues
    {
        public static List<string> Genders { get; private set; }
        public static List<bool> Bools { get; private set; }
        public static List<string> UserTypes { get; private set; }
        public static List<string> Terms { get; private set; }
        public static List<string> Majors { get; private set; }
        static EnumValues()
        {
            Genders = new List<string>() { "Male", "Female", "Other" };
            Bools = new List<bool>() { false, true };
            UserTypes = new List<string>() { "TA", "Professor" };
            Terms = new List<string>() {"Spring", "Summer", "Fall" };
            Majors = new List<string>()
            {
                "Other",
                "Accounting",
                "Anthropology",
                "Biology",
                "Criminal Justice",
                "Computer Science",
                "Computer Engineering",
                "Communication",
                "Electrical Engineering",
                "Elementary Education",
                "English",
                "Entrepreneurship",
                "Finance",
                "History",
                "Hospitality Business Management",
                "Human Development",
                "Humanities",
                "Management",
                "Marketing",
                "Mathematics",
                "Mechanical Engineering",
                "Neuroscience",
                "Nursing",
                "Psychology",
                "Public Affairs",
                "Political Science",
                "Sociology",
                "Software Engineering",
                "Strategic Communication",
                "Womens Studies"
            };
        }
    }
    public enum Terms { Spring, Summer, Fall}
    public enum GenderType { Male, Female, Other };
    public enum Bools {
        [Display(Name = "No, I have never been a TA")]
        False,
        [Display(Name = "Yes, I have experience as a TA")]
        True
    };
    public enum UserType { TA, Professor,
        [Display(AutoGenerateField=false, AutoGenerateFilter=false)]
        Student };
    public enum MajorType
    {
        Other,
        Accounting,
        Anthropology,
        Biology,
        [Display(Name = "Criminal Justice")]
        Criminal_Justice,
        [Display(Name = "Computer Science")]
        Computer_Science,
        [Display(Name = "Computer Engineering")]
        Computer_Engineering,
        Communication,
        [Display(Name = "Electrical Engineering")]
        Electrical_Engineering,
        [Display(Name = "Elementary Education")]
        Elementary_Education,
        English,
        Entrepreneurship,
        Finance,
        History,
        [Display(Name = "Hospitality Business Management")]
        Hospitality_Business_Management,
        [Display(Name = "Human Development")]
        Human_Development,
        Humanities,
        Management,
        Marketing,
        Mathematics,
        [Display(Name = "Mechanical Engineering")]
        Mechanical_Engineering,
        Neuroscience,
        Nursing,
        Psychology,
        [Display(Name = "Public Affairs")]
        Public_Affairs,
        [Display(Name = "Political Science")]
        Political_Science,
        Sociology,
        [Display(Name = "Software Engineering")]
        Software_Engineering,
        [Display(Name = "Strategic Communication")]
        Strategic_Communication,
        [Display(Name = "Womens Studies")]
        Womens_Studies
    }
    public enum PrefixTypes
    {
        [Display(Name = "Accounting")]
        ACCTG,
        [Display(Name = "Aerospace Studies")]
        AERO,
        [Display(Name = "Agricultural And Food Systems")]
        AFS,
        [Display(Name = "Agricultural Education")]
        AG_ED,
        [Display(Name = "General Agriculture")]
        AGRI,
        [Display(Name = "Agricultural Technology And Management")]
        AGTM,
        [Display(Name = "American Indian Studies")]
        AIS,
        [Display(Name = "Apparel, Merchandising, Design, And Textiles")]
        AMDT,
        [Display(Name = "American Studies Thompson")]
        AMER_ST,
        [Display(Name = "Animal Sciences")]
        ANIM_SCI,
        [Display(Name = "Anthropology")]
        ANTH,
        [Display(Name = "Architecture")]
        ARCH,
        [Display(Name = "Asia Program")]
        ASIA,
        [Display(Name = "Astronomy")]
        ASTRONOM,
        [Display(Name = "Athletic Training")]
        ATH_T,
        [Display(Name = "Business Administration")]
        B_A,
        [Display(Name = "Business Law")]
        B_LAW,
        [Display(Name = "Bioengineering")]
        BIO_ENG,
        [Display(Name = "Biology")]
        BIOLOGY,
        [Display(Name = "Biological Systems Engineering")]
        BSYSE,
        [Display(Name = "Cross-Disciplinary Arts And Sciences")]
        CAS,
        [Display(Name = "Civil Engineering")]
        CE,
        [Display(Name = "Comparative Ethnic Studies")]
        CES,
        [Display(Name = "Chemical Engineering")]
        CHE,
        [Display(Name = "Chemistry")]
        CHEM,
        [Display(Name = "Chinese")]
        CHINESE,
        [Display(Name = "Communication")]
        COM,
        [Display(Name = "Journalism And Media Production")]
        COMJOUR,
        [Display(Name = "Communication And Society")]
        COMSOC,
        [Display(Name = "Strategic Communication")]
        COMSTRAT,
        [Display(Name = "Construction Engineering")]
        CON_E,
        [Display(Name = "Counseling Psychology")]
        COUN_PSY,
        [Display(Name = "Computer Science")]
        CPT_S,
        [Display(Name = "Criminal Justice")]
        CRM_J,
        [Display(Name = " Crop Science")]
        CROP_SCI,
        [Display(Name = "Cultural Studies And Social Thought In Education")]
        CSSTE,
        [Display(Name = "Construction Managmnet")]
        CST_M,
        [Display(Name = "Digital Technology And Culture")]
        DTC,
        [Display(Name = "Education Abroad")]
        E_A,
        [Display(Name = "Electrical Engineering")]
        E_E,
        [Display(Name = "Engineering Management")]
        E_M,
        [Display(Name = "Electron Microscopy")]
        E_MIC,
        [Display(Name = "Economic Sciences")]
        ECONS,
        [Display(Name = "Educational Administration And Supervision")]
        ED_AD,
        [Display(Name = "Mathematics & Science Education")]
        ED_MTHSC,
        [Display(Name = "Educational Psychology")]
        ED_PSYCH,
        [Display(Name = "Educational Research")]
        ED_RES,
        [Display(Name = "English")]
        ENGLISH,
        [Display(Name = "Engineering")]
        ENGR,
        [Display(Name = "Entomology")]
        ENTOM,
        [Display(Name = "Entrepreneurship")]
        ENTRP,
        [Display(Name = "Finance")]
        FIN,
        [Display(Name = "Fine Arts")]
        FINE_ART,
        [Display(Name = "Foreign Languages And Cultures")]
        FOR_LANG,
        [Display(Name = "French")]
        FRENCH,
        [Display(Name = "Food Science")]
        FS,
        [Display(Name = "German")]
        GERMAN,
        [Display(Name = "Global Animal Health")]
        GLANHLTH,
        [Display(Name = "Human Development")]
        H_D,
        [Display(Name = "Hospitality Business Management")]
        HBM,
        [Display(Name = "History")]
        HISTORY,
        [Display(Name = "University Honors")]
        HONORS,
        [Display(Name = "Horticulture")]
        HORT,
        [Display(Name = "Humanities")]
        HUMANITY,
        [Display(Name = "International Business")]
        I_BUS,
        [Display(Name = "Interior Design")]
        I_D,
        [Display(Name = "Interdisciplinary")]
        INTERDIS,
        [Display(Name = "Integrated Pest Management")]
        IPM,
        [Display(Name = "International Student Exchange")]
        ISE,
        [Display(Name = "Italian")]
        ITALIAN,
        [Display(Name = "Japanese")]
        JAPANESE,
        [Display(Name = "Kinesiology")]
        KINES,
        [Display(Name = "Korean")]
        KOREAN,
        [Display(Name = "Language, Literacy, And Technology")]
        LLT,
        [Display(Name = "Landscape Architecture")]
        LND_ARCH,
        [Display(Name = "Mathematics")]
        MATH,
        [Display(Name = "Materials Science")]
        MATSE,
        [Display(Name = "Molecular Biosciences")]
        MBIOS,
        [Display(Name = "Mechanical Engineering")]
        ME,
        [Display(Name = "Managment")]
        MGMT,
        [Display(Name = "Management And Operations")]
        MGTOP,
        [Display(Name = "Military Science")]
        MIL_SCI,
        [Display(Name = "Management Information Systems")]
        MIS,
        [Display(Name = "Master In Teaching")]
        MIT,
        [Display(Name = "Marketing")]
        MKTG,
        [Display(Name = "Molecular Plant Sciences")]
        MPS,
        [Display(Name = "Materials Science And Engineering")]
        MSE,
        [Display(Name = "Music")]
        MUS,
        [Display(Name = "Neuroscience")]
        NEUROSCI,
        [Display(Name = "PE-Activity")]
        PE_ACTIV,
        [Display(Name = "Philosophy")]
        PHIL,
        [Display(Name = "Physics")]
        PHYSICS,
        [Display(Name = "Plant Pathology")]
        PL_P,
        [Display(Name = "Political Science")]
        POL_S,
        [Display(Name = "Prevention Science")]
        PREV_SCI,
        [Display(Name = "Psychology")]
        PSYCH,
        [Display(Name = "Russian")]
        RUSSIAN,
        [Display(Name = "Science")]
        SCIENCE,
        [Display(Name = "School Of Design And Construction")]
        SDC,
        [Display(Name = "Speech And Hearing Sciences")]
        SHS,
        [Display(Name = "Sociology")]
        SOC,
        [Display(Name = "School Of The Environment")]
        SOE,
        [Display(Name = "Soil Science")]
        SOIL_SCI,
        [Display(Name = "Spanish")]
        SPANISH,
        [Display(Name = "Special Education")]
        SPEC_ED,
        [Display(Name = "Sport Management")]
        SPMGT,
        [Display(Name = "Statistics")]
        STAT,
        [Display(Name = "Teaching And Learning")]
        TCH_LRN,
        [Display(Name = "University-Wide")]
        UNIV,
        [Display(Name = "Veterinary Clinical Medicine And Surgery")]
        VET_CLIN,
        [Display(Name = "Veterinary Medicine")]
        VET_MED,
        [Display(Name = "Veterinary Microbiology")]
        VET_MICR,
        [Display(Name = "Veterinary Pathology")]
        VET_PATH,
        [Display(Name = "Veterinary Physiology And Pharmacology")]
        VET_PH,
        [Display(Name = "Viticulture & Enology")]
        VIT_ENOL,
        [Display(Name = "Women's Studies")]
        WOMEN_ST,
        [Display(Name = "University Writing")]
        WRIT
    }
    //Helper-Class used to retrieve Username/password info
    public class LoginInfo
    {
        //[Required]
        public string Username { get; set; }//"firstName.lastName"
        //[Required]
        public string Password { get; set; }//Atleast 1 Letter, 8 Characters, and NO special characters
        //[Required]
        public UserType User { get; set; }
        public string Notifications { get; set; }
        public LoginInfo() { }
        public LoginInfo(LoginInfo info)
        {
            this.Username = info.Username;
            this.Password = info.Password;
            this.User = info.User;
        }
    }

    //public static class UserLogin
    //{
    //    public static int Id { get; set; }
    //    public static bool LoggedIn { get; set; }
    //    public static string Name { get; set; }
    //    public static UserType User { get; set; }
    //    public static string FullName { get; set; }
    //    public static string Email { get; set; }

    //    public static void Set(TAModel user)
    //    {
    //        LoggedIn = true;
    //        Name = user.FirstName;
    //        User = UserType.TA;
    //        Id = user.Id;
    //        Email = user.Login.Username;
    //        FullName = $"{Name} {user.LastName}";
    //        Cache.Set(user);
    //    }
    //    public static void Set(ProfessorModel user)
    //    {
    //        LoggedIn = true;
    //        Name = user.FirstName;
    //        User = UserType.Professor;
    //        Id = user.Id;
    //        Email = user.Login.Username;
    //        FullName = $"{Name} {user.LastName}";
    //        Cache.Set(user);
    //    }
    //    public static void Reset()
    //    {
    //        LoggedIn = false;
    //        Name = String.Empty;
    //        Id = default(int);
    //        User = default(UserType);
    //        Email = String.Empty;
    //        FullName = String.Empty;
    //        Cache.RemoveUser();
    //    }
    //}
}