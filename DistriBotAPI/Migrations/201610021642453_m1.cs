namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Salesman_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Salesman_Id");
            AddForeignKey("dbo.Orders", "Salesman_Id", "dbo.Salesmen", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Salesman_Id", "dbo.Salesmen");
            DropIndex("dbo.Orders", new[] { "Salesman_Id" });
            DropColumn("dbo.Orders", "Salesman_Id");
        }
    }
}
