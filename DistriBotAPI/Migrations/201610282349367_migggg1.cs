namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migggg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "DayOfWeek", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "DayOfWeek");
        }
    }
}
