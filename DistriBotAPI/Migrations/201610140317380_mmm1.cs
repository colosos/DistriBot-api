namespace DistriBotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mmm1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Devolution_Id", c => c.Int());
            CreateIndex("dbo.Items", "Devolution_Id");
            AddForeignKey("dbo.Items", "Devolution_Id", "dbo.Devolutions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Devolution_Id", "dbo.Devolutions");
            DropIndex("dbo.Items", new[] { "Devolution_Id" });
            DropColumn("dbo.Items", "Devolution_Id");
        }
    }
}
