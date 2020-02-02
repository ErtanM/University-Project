namespace GalleryApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Markas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModelDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MarkaID = c.Int(nullable: false),
                        ModelID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Engine = c.String(),
                        FuelType = c.Short(nullable: false),
                        GearBoxType = c.Short(nullable: false),
                        Colour = c.Short(nullable: false),
                        BodyType = c.Short(nullable: false),
                        Status = c.Short(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);

        }
        
        public override void Down()
        {
            DropTable("dbo.ModelDetails");
            DropTable("dbo.Models");
            DropTable("dbo.Markas");
        }
    }
}
