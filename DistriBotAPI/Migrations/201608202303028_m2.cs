namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Address", c => c.String());
            AddColumn("dbo.Clients", "Phone", c => c.String());
            AddColumn("dbo.Clients", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "EmailAddress");
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "Address");
        }
    }
}
