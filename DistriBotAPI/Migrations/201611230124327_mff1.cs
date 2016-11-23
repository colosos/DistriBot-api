namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mff1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Routes", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "Active", c => c.Boolean(nullable: false));
        }
    }
}
