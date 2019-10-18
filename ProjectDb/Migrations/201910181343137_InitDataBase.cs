namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vagons", "BusyPaces", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vagons", "BusyPaces");
        }
    }
}
