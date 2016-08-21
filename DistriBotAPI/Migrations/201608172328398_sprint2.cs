namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprint2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        DeliveredDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.BaseProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "CreditBalance", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Description", c => c.String());
            AddColumn("dbo.Products", "Unit", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "UnitQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "BaseProduct_Id", c => c.Int());
            CreateIndex("dbo.Products", "BaseProduct_Id");
            AddForeignKey("dbo.Products", "BaseProduct_Id", "dbo.BaseProducts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BaseProduct_Id", "dbo.BaseProducts");
            DropForeignKey("dbo.Orders", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "BaseProduct_Id" });
            DropIndex("dbo.Orders", new[] { "Client_Id" });
            DropColumn("dbo.Products", "BaseProduct_Id");
            DropColumn("dbo.Products", "UnitQuantity");
            DropColumn("dbo.Products", "Unit");
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Clients", "CreditBalance");
            DropTable("dbo.BaseProducts");
            DropTable("dbo.Orders");
        }
    }
}
