namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigValid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vagons", "Type", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Stations", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Vagons", "Type", c => c.String(maxLength: 100));
        }
    }
}
