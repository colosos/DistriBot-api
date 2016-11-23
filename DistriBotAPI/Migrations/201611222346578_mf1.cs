namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mf1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "Active");
        }
    }
}
