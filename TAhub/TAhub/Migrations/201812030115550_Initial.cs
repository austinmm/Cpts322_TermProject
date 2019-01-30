namespace TAhub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SLN = c.Int(nullable: false),
                        Prefix = c.String(nullable: false),
                        CourseNumber = c.Int(nullable: false),
                        EnrollmentLimit = c.Int(nullable: false),
                        CourseTitle = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Term = c.String(nullable: false),
                        ProfessorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professors", t => t.ProfessorId, cascadeDelete: true)
                .Index(t => t.ProfessorId);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeachersAssistants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Major = c.String(nullable: false),
                        GPA = c.String(nullable: false),
                        Credits = c.Int(nullable: false),
                        Preference1 = c.String(),
                        Preference2 = c.String(),
                        Preference3 = c.String(),
                        HasExperience = c.Boolean(nullable: false),
                        Lab = c.Int(),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderName = c.String(nullable: false),
                        SenderEmail = c.String(nullable: false),
                        SenderType = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        TeachersAssistantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeachersAssistants", t => t.TeachersAssistantId, cascadeDelete: true)
                .Index(t => t.TeachersAssistantId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seven_AM = c.Boolean(nullable: false),
                        Eight_AM = c.Boolean(nullable: false),
                        Nine_AM = c.Boolean(nullable: false),
                        Ten_AM = c.Boolean(nullable: false),
                        Eleven_AM = c.Boolean(nullable: false),
                        Twelve_PM = c.Boolean(nullable: false),
                        One_PM = c.Boolean(nullable: false),
                        Two_PM = c.Boolean(nullable: false),
                        Three_PM = c.Boolean(nullable: false),
                        Four_PM = c.Boolean(nullable: false),
                        Five_PM = c.Boolean(nullable: false),
                        Six_PM = c.Boolean(nullable: false),
                        Seven_PM = c.Boolean(nullable: false),
                        TeachersAssistantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeachersAssistants", t => t.TeachersAssistantId, cascadeDelete: true)
                .Index(t => t.TeachersAssistantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "TeachersAssistantId", "dbo.TeachersAssistants");
            DropForeignKey("dbo.Messages", "TeachersAssistantId", "dbo.TeachersAssistants");
            DropForeignKey("dbo.TeachersAssistants", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "ProfessorId", "dbo.Professors");
            DropIndex("dbo.Schedules", new[] { "TeachersAssistantId" });
            DropIndex("dbo.Messages", new[] { "TeachersAssistantId" });
            DropIndex("dbo.TeachersAssistants", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "ProfessorId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Messages");
            DropTable("dbo.TeachersAssistants");
            DropTable("dbo.Professors");
            DropTable("dbo.Courses");
        }
    }
}
