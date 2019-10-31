namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataValidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "IsRemoved");
        }
    }
}
