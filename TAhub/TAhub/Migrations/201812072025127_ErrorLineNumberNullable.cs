namespace TAhub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorLineNumberNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Errors", "LineNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Errors", "LineNumber", c => c.Int(nullable: false));
        }
    }
}
