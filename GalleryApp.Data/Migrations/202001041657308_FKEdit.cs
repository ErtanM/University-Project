namespace GalleryApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Models", "MarkaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Models", "MarkaID");
            CreateIndex("dbo.ModelDetails", "ModelID");
            AddForeignKey("dbo.Models", "MarkaID", "dbo.Markas", "ID");
            AddForeignKey("dbo.ModelDetails", "ModelID", "dbo.Models", "ID");
            DropColumn("dbo.ModelDetails", "MarkaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelDetails", "MarkaID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ModelDetails", "ModelID", "dbo.Models");
            DropForeignKey("dbo.Models", "MarkaID", "dbo.Markas");
            DropIndex("dbo.ModelDetails", new[] { "ModelID" });
            DropIndex("dbo.Models", new[] { "MarkaID" });
            DropColumn("dbo.Models", "MarkaID");
        }
    }
}
