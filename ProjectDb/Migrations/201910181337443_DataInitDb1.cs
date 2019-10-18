namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataInitDb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vagons", "EmptyPlaces", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vagons", "EmptyPlaces");
        }
    }
}
