namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMig : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Routes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "Name", c => c.String());
        }
    }
}
