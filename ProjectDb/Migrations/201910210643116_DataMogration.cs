namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMogration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChosenTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        VagonId = c.Int(nullable: false),
                        TrainId = c.Int(nullable: false),
                        UserEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChosenTickets");
        }
    }
}
