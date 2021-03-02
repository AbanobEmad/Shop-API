namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12oper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Single_Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Orders_Id = c.Int(nullable: false),
                        Size_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.Orders_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_Id)
                .Index(t => t.Product_ID)
                .Index(t => t.Orders_Id)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_ID = c.String(maxLength: 128),
                        Send = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Single_Order", "Size_Id", "dbo.Size");
            DropForeignKey("dbo.Single_Order", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Orders", "User_ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Single_Order", "Orders_Id", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropIndex("dbo.Single_Order", new[] { "Size_Id" });
            DropIndex("dbo.Single_Order", new[] { "Orders_Id" });
            DropIndex("dbo.Single_Order", new[] { "Product_ID" });
            DropTable("dbo.Orders");
            DropTable("dbo.Single_Order");
        }
    }
}
