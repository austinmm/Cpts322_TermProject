namespace TAhub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWeeklyScheduleFieldforTA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "Course", c => c.String(nullable: false));
            AddColumn("dbo.Schedules", "Time", c => c.String(nullable: false));
            AddColumn("dbo.Schedules", "Days", c => c.String(nullable: false));
            DropColumn("dbo.Schedules", "Seven_AM");
            DropColumn("dbo.Schedules", "Eight_AM");
            DropColumn("dbo.Schedules", "Nine_AM");
            DropColumn("dbo.Schedules", "Ten_AM");
            DropColumn("dbo.Schedules", "Eleven_AM");
            DropColumn("dbo.Schedules", "Twelve_PM");
            DropColumn("dbo.Schedules", "One_PM");
            DropColumn("dbo.Schedules", "Two_PM");
            DropColumn("dbo.Schedules", "Three_PM");
            DropColumn("dbo.Schedules", "Four_PM");
            DropColumn("dbo.Schedules", "Five_PM");
            DropColumn("dbo.Schedules", "Six_PM");
            DropColumn("dbo.Schedules", "Seven_PM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "Seven_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Six_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Five_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Four_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Three_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Two_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "One_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Twelve_PM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Eleven_AM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Ten_AM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Nine_AM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Eight_AM", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedules", "Seven_AM", c => c.Boolean(nullable: false));
            DropColumn("dbo.Schedules", "Days");
            DropColumn("dbo.Schedules", "Time");
            DropColumn("dbo.Schedules", "Course");
        }
    }
}
