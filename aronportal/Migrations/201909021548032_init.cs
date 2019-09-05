namespace aronportal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FixtureCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FixtureCategoryName = c.String(),
                        FixtureCategoryCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fixtures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FixtureName = c.String(),
                        FixtureCost = c.Decimal(precision: 18, scale: 2),
                        FixtureCode = c.String(),
                        FixtureCategoryId = c.Int(),
                        FixtureCategories_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FixtureCategories", t => t.FixtureCategories_Id)
                .Index(t => t.FixtureCategories_Id);
            
            CreateTable(
                "dbo.FixtureParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FixtureId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Cost = c.Int(),
                        Fixtures_Id = c.Int(),
                        Parts_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fixtures", t => t.Fixtures_Id)
                .ForeignKey("dbo.Parts", t => t.Parts_Id)
                .Index(t => t.Fixtures_Id)
                .Index(t => t.Parts_Id);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartName = c.String(),
                        PartDescription = c.String(),
                        PartVendor = c.String(),
                        PartCost = c.Decimal(precision: 18, scale: 2),
                        PartQuantity = c.Int(),
                        InventoryCategoryId = c.Int(),
                        ItemGroupsId = c.Int(),
                        PartNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InventoryCategories", t => t.InventoryCategoryId)
                .ForeignKey("dbo.ItemGroups", t => t.ItemGroupsId)
                .Index(t => t.InventoryCategoryId)
                .Index(t => t.ItemGroupsId);
            
            CreateTable(
                "dbo.InventoryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InventoryCategoryName = c.String(),
                        InventoryCategoryCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemGroupName = c.String(),
                        ItemGroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Poparts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartId = c.Int(),
                        Quantity = c.Int(),
                        PurchaseOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parts", t => t.PartId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PartId)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ponumber = c.String(),
                        PorecDate = c.DateTime(),
                        PoestShipDate = c.DateTime(),
                        PoactShipDate = c.DateTime(),
                        Pocost = c.Decimal(precision: 18, scale: 2),
                        Poprice = c.Decimal(precision: 18, scale: 2),
                        Pocompleted = c.Boolean(),
                        CreatedAt = c.DateTime(),
                        CustomerId = c.Short(nullable: false),
                        Poname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pofixtures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(),
                        FixtureId = c.Int(),
                        FixturePrice = c.Decimal(precision: 18, scale: 2),
                        FixtureCommision = c.Decimal(precision: 18, scale: 2),
                        FixtureQuantity = c.Int(),
                        FixtureQuantityCompleted = c.Int(),
                        FixtureQuantityShipped = c.Int(),
                        FixtureCost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fixtures", t => t.FixtureId)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.FixtureId);
            
            CreateTable(
                "dbo.TblUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Password = c.Binary(),
                        Salt = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorName = c.String(),
                        VendorNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Poparts", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.Pofixtures", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.Pofixtures", "FixtureId", "dbo.Fixtures");
            DropForeignKey("dbo.Poparts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Parts", "ItemGroupsId", "dbo.ItemGroups");
            DropForeignKey("dbo.Parts", "InventoryCategoryId", "dbo.InventoryCategories");
            DropForeignKey("dbo.FixtureParts", "Parts_Id", "dbo.Parts");
            DropForeignKey("dbo.FixtureParts", "Fixtures_Id", "dbo.Fixtures");
            DropForeignKey("dbo.Fixtures", "FixtureCategories_Id", "dbo.FixtureCategories");
            DropIndex("dbo.Pofixtures", new[] { "FixtureId" });
            DropIndex("dbo.Pofixtures", new[] { "PurchaseOrderId" });
            DropIndex("dbo.Poparts", new[] { "PurchaseOrderId" });
            DropIndex("dbo.Poparts", new[] { "PartId" });
            DropIndex("dbo.Parts", new[] { "ItemGroupsId" });
            DropIndex("dbo.Parts", new[] { "InventoryCategoryId" });
            DropIndex("dbo.FixtureParts", new[] { "Parts_Id" });
            DropIndex("dbo.FixtureParts", new[] { "Fixtures_Id" });
            DropIndex("dbo.Fixtures", new[] { "FixtureCategories_Id" });
            DropTable("dbo.Vendors");
            DropTable("dbo.TblUsers");
            DropTable("dbo.Pofixtures");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.Poparts");
            DropTable("dbo.ItemGroups");
            DropTable("dbo.InventoryCategories");
            DropTable("dbo.Parts");
            DropTable("dbo.FixtureParts");
            DropTable("dbo.Fixtures");
            DropTable("dbo.FixtureCategories");
        }
    }
}
