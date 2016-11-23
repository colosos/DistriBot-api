namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miggg1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "Route_Id", "dbo.Routes");
            DropIndex("dbo.Clients", new[] { "Route_Id" });
            CreateTable(
                "dbo.RouteClients",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.Client_Id })
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.Client_Id);
            
            DropColumn("dbo.Clients", "Route_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Route_Id", c => c.Int());
            DropForeignKey("dbo.RouteClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.RouteClients", "Route_Id", "dbo.Routes");
            DropIndex("dbo.RouteClients", new[] { "Client_Id" });
            DropIndex("dbo.RouteClients", new[] { "Route_Id" });
            DropTable("dbo.RouteClients");
            CreateIndex("dbo.Clients", "Route_Id");
            AddForeignKey("dbo.Clients", "Route_Id", "dbo.Routes", "Id");
        }
    }
}
