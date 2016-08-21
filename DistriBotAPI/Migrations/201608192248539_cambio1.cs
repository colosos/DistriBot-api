namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseProduct_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseProducts", t => t.BaseProduct_Id)
                .Index(t => t.BaseProduct_Id);
            
            CreateTable(
                "dbo.Devolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        DevolutionDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Driver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryMen", t => t.Driver_Id)
                .Index(t => t.Driver_Id);
            
            AddColumn("dbo.Clients", "Route_Id", c => c.Int());
            AlterColumn("dbo.Products", "UnitQuantity", c => c.Double(nullable: false));
            CreateIndex("dbo.Clients", "Route_Id");
            AddForeignKey("dbo.Clients", "Route_Id", "dbo.Routes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "Driver_Id", "dbo.DeliveryMen");
            DropForeignKey("dbo.Clients", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Devolutions", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Tags", "BaseProduct_Id", "dbo.BaseProducts");
            DropIndex("dbo.Routes", new[] { "Driver_Id" });
            DropIndex("dbo.Devolutions", new[] { "Client_Id" });
            DropIndex("dbo.Clients", new[] { "Route_Id" });
            DropIndex("dbo.Tags", new[] { "BaseProduct_Id" });
            AlterColumn("dbo.Products", "UnitQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "Route_Id");
            DropTable("dbo.Routes");
            DropTable("dbo.Devolutions");
            DropTable("dbo.Tags");
        }
    }
}
