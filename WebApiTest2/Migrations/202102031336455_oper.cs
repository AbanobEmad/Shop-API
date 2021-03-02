namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oper : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category_Type", "Category_ID", "dbo.Category");
            DropForeignKey("dbo.Category_Type", "Type_ID", "dbo.Type");
            DropIndex("dbo.Category_Type", new[] { "Category_ID" });
            DropIndex("dbo.Category_Type", new[] { "Type_ID" });
            CreateTable(
                "dbo.Category_Gender",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Categoer_Id = c.Int(nullable: false),
                        Gender_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Categoer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Gender", t => t.Gender_ID, cascadeDelete: true)
                .Index(t => t.Categoer_Id)
                .Index(t => t.Gender_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Discount = c.Single(nullable: false),
                        Offer = c.Boolean(nullable: false),
                        percent = c.Single(nullable: false),
                        Category_TD = c.Int(nullable: false),
                        Seller_ID = c.Int(nullable: false),
                        Type_ID = c.Int(nullable: false),
                        Save = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Category_TD, cascadeDelete: true)
                .ForeignKey("dbo.Type", t => t.Type_ID, cascadeDelete: true)
                .Index(t => t.Category_TD)
                .Index(t => t.Type_ID);
            
            CreateTable(
                "dbo.Product_Image_path",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Image_path_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Image_Path", t => t.Image_path_ID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Image_path_ID);
            
            CreateTable(
                "dbo.Image_Path",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Category_Type");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Category_Type",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category_ID = c.Int(),
                        Type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Product", "Type_ID", "dbo.Type");
            DropForeignKey("dbo.Product_Image_path", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Product_Image_path", "Image_path_ID", "dbo.Image_Path");
            DropForeignKey("dbo.Product", "Category_TD", "dbo.Category");
            DropForeignKey("dbo.Category_Gender", "Gender_ID", "dbo.Gender");
            DropForeignKey("dbo.Category_Gender", "Categoer_Id", "dbo.Category");
            DropIndex("dbo.Product_Image_path", new[] { "Image_path_ID" });
            DropIndex("dbo.Product_Image_path", new[] { "Product_ID" });
            DropIndex("dbo.Product", new[] { "Type_ID" });
            DropIndex("dbo.Product", new[] { "Category_TD" });
            DropIndex("dbo.Category_Gender", new[] { "Gender_ID" });
            DropIndex("dbo.Category_Gender", new[] { "Categoer_Id" });
            DropTable("dbo.Image_Path");
            DropTable("dbo.Product_Image_path");
            DropTable("dbo.Product");
            DropTable("dbo.Category_Gender");
            CreateIndex("dbo.Category_Type", "Type_ID");
            CreateIndex("dbo.Category_Type", "Category_ID");
            AddForeignKey("dbo.Category_Type", "Type_ID", "dbo.Type", "ID");
            AddForeignKey("dbo.Category_Type", "Category_ID", "dbo.Category", "ID");
        }
    }
}
