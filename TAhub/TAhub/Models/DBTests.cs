using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.IO;

namespace TAhub.DB
{
    public static class DBTests
    {
        private static List<string> FirstNames_Male = new List<string>()
        {
            "Liam","Noah","William","James","Logan","Benjamin","Mason","Owen","John","Joshua",
            "Elijah","Oliver","Jacob","Lucas","Michael","Alexander","Ethan","Daniel","Matthew",
            "Aiden","Henry","Joseph","Jackson","Samuel","Sebastian","David","Carter","Wyatt","Jayden",
            "Dylan","Luke","Gabriel","Anthony","Isaac","Grayson","Jack","Julian","Levi","Christopher",
            "Andrew","Lincoln","Mateo","Ryan","Jaxon","Nathan","Aaron","Isaiah","Thomas","Charles","Caleb"
        };
        private static List<string> FirstNames_Female = new List<string>()
        {
            "Emma","Olivia","Ava","Isabella","Sophia","Mia","Charlotte","Amelia","Evelyn","Aria",
            "Abigail","Harper","Emily","Elizabeth","Avery","Sofia","Ella","Madison","Scarlett",
            "Grace","Chloe","Camila","Penelope","Riley","Layla","Lillian","Nora","Zoey","Mila","Aubrey",
            "Hannah","Lily","Addison","Eleanor","Natalie","Luna","Savannah","Brooklyn","Leah","Zoe",
            "Hazel","Ellie","Paisley","Audrey","Skylar","Violet","Claire","Bella","Stella","Victoria"
        };

        private static List<string> LastNames = new List<string>()
        {
            "Smith","Johnson","Williams","Jones","Brown","Davis","Miller","Wilson","Moore","Taylor",
            "Jackson","White","Harris","Martin","Thompson","Garcia","Martinez","Robinson","Clark",
            "Rodriguez","Lewis","Lee","Walker","Hall","Allen","Young","Hernandez","King","Wright",
            "Lopez","Hill","Scott","Green","Adams","Baker","Gonzalez","Nelson","Carter","Mitchell","Perez",
            "Roberts","Turner","Phillips","Campbell","Parker","Evans","Edwards","Collins","Anderson","Thomas",
            "Stewart","Sanchez","Morris","Rogers","Reed","Cook","Morgan","Bell","Murphy",
            "Rivera","Cooper","Richardson","Cox","Howard","Ward","Torres","Peterson",
            "Gray","Ramirez","James","Watson","Brooks","Kelly","Sanders","Price","Bailey",
            "Bennett","Wood","Barnes","Ross","Henderson","Coleman","Jenkins","Perry",
            "Powell","Long","Patterson","Hughes","Flores","Washington","Butler","Simmons",
            "Foster","Gonzales","Bryant","Alexander","Russell","Griffin","Diaz","Hayes"
        };

        private static List<string> Messages = new List<string>()
        {
            "Hello, I really need help on the current assignment and was hoping I could meet with you sometime later this week. Hope to hear from you soon, Thanks!",
            "Hey, as you know the Midterm for our class is next Tuesday; however, I don't feel comfortable with the newest chapter we learned in lecture."
            + " Thus, I was wondering if you were free anytime this week or early next week to meet up and help me with the new course material. Thanks!",
            "Hello There! I was looking over my exam we got back in lecture today and noticed I got deducted 5 points on number 7 for not simplifying my answer all the way."
            + " I was never informed that we had to simplify our answers down as it was not stated anywhere on the exam. Is there any way I can get some points back?"
        };

        private static List<int> CourseSLNs = new List<int>()
        {
            04779, 06304, 06307, 04617, 03444, 03445, 06416, 04266, 07293, 08266,
            03775, 07854, 04605, 04835, 06859, 04846, 04860, 03659, 07819, 11951,
            05761, 05762, 05308, 05310, 05313, 06838, 08106, 08107, 05317, 05318,
            06490, 05321, 06843, 06845, 05323, 07303, 05328, 05329, 03392, 07248,
            03394, 03399, 03408, 04690, 04700, 04711, 04722, 04726, 04732, 04735
        };

        public static void CreateTestData()
        {
            List<TeachersAssistant> TAs = Generate_TAs(100);
            List<Professor> Profs = Generate_Profs(50);
            List<Course> Courses = Generate_Courses();
            int iTA = 0, TA_MaxSize = TAs.Count-50, iProf = 0, Prof_MaxSize = Profs.Count;
            for(int i = TAs.Count-1; i > TAs.Count-50; i--)
            {
                TAs[i].Lab = null;
                DB.DBMethods.Insert(TAs[i]);
            }
            foreach (Course course in Courses)
            {
                if (iProf < Prof_MaxSize)
                {
                    if (iTA < TA_MaxSize)
                    {
                        TAs[iTA].Lab = new Random().Next(1, 15);
                        course.TAs = new List<TeachersAssistant>();
                        course.TAs.Add(TAs[iTA++]);
                    }
                    if (iTA < TA_MaxSize)
                    {
                        TAs[iTA].Lab = new Random().Next(1, 15);
                        course.TAs.Add(TAs[iTA++]);
                    }
                    course.Professor = Profs[iProf++];
                    DB.DBMethods.Insert(course);
                }
            }
        }

        public static List<Message> Generate_Messages()
        {
            List<Message> messages = new List<Message>();
            int i = 0;
            Random rand = new Random();
            foreach (string message in Messages)
            {
                messages.Add(new Message());
                int num = rand.Next(0, FirstNames_Male.Count);
                bool isMale = num % 2 == 0;
                string fName = FirstNames_Male[num % FirstNames_Male.Count];
                string lName = LastNames[num % LastNames.Count];
                if (isMale)
                {
                    messages[i].SenderName = $"{fName} {lName}";
                }
                else
                {
                    fName = FirstNames_Female[num % 50];
                    messages[i].SenderName = $"{fName} {lName}";
                }
                messages[i].SenderEmail = $"{fName}.{lName}@wsu.edu";
                messages[i].SenderType = Models.EnumValues.UserTypes[(int)Models.UserType.Student];
                messages[i].Text = message;
                i++;
            }
            return messages;
        }


        public static List<TeachersAssistant> Generate_TAs(int Count)
        {
            List<TeachersAssistant> TAs = new List<TeachersAssistant>();
            for(int i = 0; i < Count; i++)
            {
                TeachersAssistant TA = new TeachersAssistant();
                TA.Password = "password1";
                TA.StudentId = 11507800 + i;
                TA.GPA = $"3.{i%10}{(i+7)%10}";
                int majorIndex = i % TAhub.Models.EnumValues.Majors.Count;
                TA.Major = TAhub.Models.EnumValues.Majors[majorIndex];
                TA.HasExperience = true;
                TA.Credits = 25 + (i % 50);
                bool isMale = i % 2 == 0;
                if (isMale)
                {
                    TA.FirstName = FirstNames_Male[i% FirstNames_Male.Count];
                    TA.Gender = "Male";
                }
                else
                {
                    TA.FirstName = FirstNames_Female[i% FirstNames_Female.Count];
                    TA.Gender = "Female";
                }
                TA.LastName = LastNames[i%LastNames.Count];
                TA.Email = $"{TA.FirstName}.{TA.LastName}@wsu.edu";
                TA.Messages = Generate_Messages();
                TA.Preference1 = "Cpts 121";
                TA.Preference2 = "Psych 230";
                TA.Preference3 = "Cpts 322";
                TAs.Add(TA);
            }
            return TAs;
        }

        public static List<Professor> Generate_Profs(int Count)
        {
            List<Professor> Profs = new List<Professor>();
            for (int i = 0; i < Count; i++)
            {
                Professor Prof = new Professor();
                Prof.Password = "password1";
                bool isMale = i % 2 == 0;
                if (isMale)
                {
                    Prof.FirstName = FirstNames_Male[i% FirstNames_Male.Count];
                }
                else
                {
                    Prof.FirstName = FirstNames_Female[i% FirstNames_Female.Count];
                }
                Prof.LastName = LastNames[i%LastNames.Count];
                Prof.Email = $"{Prof.FirstName}.{Prof.LastName}@wsu.edu";
                Profs.Add(Prof);
            }
            return Profs;
        }


        public static List<Course> Generate_Courses()
        {
            List<Course> courses = new List<Course>();
            foreach (int sln in CourseSLNs)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string URL = "http://www.schedules.wsu.edu/api/ClassBySLN/20191/";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    // List data response.
                    HttpResponseMessage response = client.GetAsync($"{sln}").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            // Parse the response body.
                            using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                            {
                                Course course = new Course();
                                StreamReader sr = new StreamReader(stream);
                                JObject json = JObject.Parse(sr.ReadToEnd());
                                course.SLN = Int32.Parse(json["Classes"][0]["SLN"].ToString());
                                course.Prefix = json["Classes"][0]["Prefix"].ToString();
                                course.CourseNumber = Int32.Parse(json["Classes"][0]["CourseNumber"].ToString());
                                course.CourseTitle = json["Classes"][0]["CourseTitle"].ToString();
                                course.EnrollmentLimit = Int32.Parse(json["Classes"][0]["EnrollmentLimit"].ToString());
                                int index = Int32.Parse(json["Term"].ToString());
                                course.Term = Models.EnumValues.Terms[index - 1];
                                course.Year = Int32.Parse(json["Year"].ToString());
                                courses.Add(course);
                            }
                        }
                        catch (Exception)
                        {
                            Debug.Print("Failed to create Course");
                        }
                    }
                    else
                    {
                        Debug.Print("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                }
            }
            //Make any other calls using HttpClient here.
            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            return courses;
        }
    }
}