namespace TAhub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedErrorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        HasInnerException = c.Boolean(nullable: false),
                        InnerExceptionMessage = c.String(),
                        LineNumber = c.Int(nullable: false),
                        FileName = c.String(),
                        NameSpace = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Errors");
        }
    }
}
