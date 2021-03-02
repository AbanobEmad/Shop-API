namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3oper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SizeOFProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Size_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Max_C = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_ID, cascadeDelete: true)
                .Index(t => t.Size_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Size",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        size = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SizeOFProduct", "Size_ID", "dbo.Size");
            DropForeignKey("dbo.SizeOFProduct", "Product_ID", "dbo.Product");
            DropIndex("dbo.SizeOFProduct", new[] { "Product_ID" });
            DropIndex("dbo.SizeOFProduct", new[] { "Size_ID" });
            DropTable("dbo.Size");
            DropTable("dbo.SizeOFProduct");
        }
    }
}
