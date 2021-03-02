namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4oper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        User_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "User_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carts", "Product_ID", "dbo.Product");
            DropIndex("dbo.Carts", new[] { "User_ID" });
            DropIndex("dbo.Carts", new[] { "Product_ID" });
            DropTable("dbo.Carts");
        }
    }
}
