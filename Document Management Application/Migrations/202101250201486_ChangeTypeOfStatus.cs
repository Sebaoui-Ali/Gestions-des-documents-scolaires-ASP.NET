namespace Document_Management_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Status", c => c.String(maxLength: 30));
        }
    }
}
