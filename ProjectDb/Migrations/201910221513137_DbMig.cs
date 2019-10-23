namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "VagonNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "RouteName", c => c.String());
            AddColumn("dbo.Tickets", "TrainNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "VagonType", c => c.String());
            AddColumn("dbo.Tickets", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Email");
            DropColumn("dbo.Tickets", "VagonType");
            DropColumn("dbo.Tickets", "TrainNumber");
            DropColumn("dbo.Tickets", "RouteName");
            DropColumn("dbo.Tickets", "VagonNumber");
        }
    }
}
