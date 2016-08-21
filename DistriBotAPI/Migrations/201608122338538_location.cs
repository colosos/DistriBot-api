namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Clients", "Longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Clients", "Latitude", c => c.Double(nullable: false));
        }
    }
}
