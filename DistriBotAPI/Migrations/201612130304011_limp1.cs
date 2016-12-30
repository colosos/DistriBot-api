namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class limp1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "BaseProduct_Id", "dbo.BaseProducts");
            DropForeignKey("dbo.Devolutions", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Products", "BaseProduct_Id", "dbo.BaseProducts");
            DropForeignKey("dbo.Items", "Devolution_Id", "dbo.Devolutions");
            DropIndex("dbo.Tags", new[] { "BaseProduct_Id" });
            DropIndex("dbo.Devolutions", new[] { "Client_Id" });
            DropIndex("dbo.Items", new[] { "Devolution_Id" });
            DropIndex("dbo.Products", new[] { "BaseProduct_Id" });
            DropColumn("dbo.Items", "Devolution_Id");
            DropColumn("dbo.Products", "BaseProduct_Id");
            DropTable("dbo.BaseProducts");
            DropTable("dbo.Tags");
            DropTable("dbo.Devolutions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Devolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        DevolutionDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseProduct_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BaseProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "BaseProduct_Id", c => c.Int());
            AddColumn("dbo.Items", "Devolution_Id", c => c.Int());
            CreateIndex("dbo.Products", "BaseProduct_Id");
            CreateIndex("dbo.Items", "Devolution_Id");
            CreateIndex("dbo.Devolutions", "Client_Id");
            CreateIndex("dbo.Tags", "BaseProduct_Id");
            AddForeignKey("dbo.Items", "Devolution_Id", "dbo.Devolutions", "Id");
            AddForeignKey("dbo.Products", "BaseProduct_Id", "dbo.BaseProducts", "Id");
            AddForeignKey("dbo.Devolutions", "Client_Id", "dbo.Clients", "Id");
            AddForeignKey("dbo.Tags", "BaseProduct_Id", "dbo.BaseProducts", "Id");
        }
    }
}
