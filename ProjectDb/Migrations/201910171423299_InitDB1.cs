namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "FinishStation_Id", "dbo.Stations");
            DropForeignKey("dbo.Routes", "StartStation_Id", "dbo.Stations");
            DropForeignKey("dbo.Stations", "Route_Id", "dbo.Routes");
            DropIndex("dbo.Routes", new[] { "FinishStation_Id" });
            DropIndex("dbo.Routes", new[] { "StartStation_Id" });
            DropIndex("dbo.Stations", new[] { "Route_Id" });
            DropColumn("dbo.Routes", "FinishStation_Id");
            DropColumn("dbo.Routes", "StartStation_Id");
            DropColumn("dbo.Stations", "Route_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "Route_Id", c => c.Int());
            AddColumn("dbo.Routes", "StartStation_Id", c => c.Int());
            AddColumn("dbo.Routes", "FinishStation_Id", c => c.Int());
            CreateIndex("dbo.Stations", "Route_Id");
            CreateIndex("dbo.Routes", "StartStation_Id");
            CreateIndex("dbo.Routes", "FinishStation_Id");
            AddForeignKey("dbo.Stations", "Route_Id", "dbo.Routes", "Id");
            AddForeignKey("dbo.Routes", "StartStation_Id", "dbo.Stations", "Id");
            AddForeignKey("dbo.Routes", "FinishStation_Id", "dbo.Stations", "Id");
        }
    }
}
