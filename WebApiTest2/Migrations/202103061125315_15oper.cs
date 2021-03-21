namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15oper : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Carts", newName: "Cart");
            RenameColumn(table: "dbo.Product", name: "Category_TD", newName: "Category_ID");
            RenameIndex(table: "dbo.Product", name: "IX_Category_TD", newName: "IX_Category_ID");
            CreateTable(
                "dbo.CategorySize",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Categoer_ID = c.Int(nullable: false),
                        Size_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Categoer_ID, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_ID, cascadeDelete: true)
                .Index(t => t.Categoer_ID)
                .Index(t => t.Size_ID);
            
            CreateTable(
                "dbo.Seller",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategorySize", "Size_ID", "dbo.Size");
            DropForeignKey("dbo.CategorySize", "Categoer_ID", "dbo.Category");
            DropIndex("dbo.CategorySize", new[] { "Size_ID" });
            DropIndex("dbo.CategorySize", new[] { "Categoer_ID" });
            DropTable("dbo.Seller");
            DropTable("dbo.CategorySize");
            RenameIndex(table: "dbo.Product", name: "IX_Category_ID", newName: "IX_Category_TD");
            RenameColumn(table: "dbo.Product", name: "Category_ID", newName: "Category_TD");
            RenameTable(name: "dbo.Cart", newName: "Carts");
        }
    }
}
