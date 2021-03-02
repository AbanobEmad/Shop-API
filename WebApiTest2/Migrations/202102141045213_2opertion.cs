namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2opertion : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Category_Gender", new[] { "Categoer_Id" });
            RenameColumn(table: "dbo.DataUsers", name: "UserID", newName: "User_ID");
            RenameIndex(table: "dbo.DataUsers", name: "IX_UserID", newName: "IX_User_ID");
            CreateTable(
                "dbo.Saved",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        User_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.User_ID);
            
            CreateIndex("dbo.Category_Gender", "Categoer_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Saved", "User_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Saved", "Product_ID", "dbo.Product");
            DropIndex("dbo.Saved", new[] { "User_ID" });
            DropIndex("dbo.Saved", new[] { "Product_ID" });
            DropIndex("dbo.Category_Gender", new[] { "Categoer_ID" });
            DropTable("dbo.Saved");
            RenameIndex(table: "dbo.DataUsers", name: "IX_User_ID", newName: "IX_UserID");
            RenameColumn(table: "dbo.DataUsers", name: "User_ID", newName: "UserID");
            CreateIndex("dbo.Category_Gender", "Categoer_Id");
        }
    }
}
