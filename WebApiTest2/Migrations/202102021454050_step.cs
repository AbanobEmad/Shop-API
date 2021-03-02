namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category_gender", "Category_ID", "dbo.Category");
            DropForeignKey("dbo.Category_gender", "Gender_ID", "dbo.Gender");
            DropIndex("dbo.Category_gender", new[] { "Category_ID" });
            DropIndex("dbo.Category_gender", new[] { "Gender_ID" });
            DropIndex("dbo.Orders", new[] { "customerId" });
            CreateIndex("dbo.Orders", "CustomerID");
            DropTable("dbo.Category_gender");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Category_gender",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category_ID = c.Int(),
                        Gender_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            CreateIndex("dbo.Orders", "customerId");
            CreateIndex("dbo.Category_gender", "Gender_ID");
            CreateIndex("dbo.Category_gender", "Category_ID");
            AddForeignKey("dbo.Category_gender", "Gender_ID", "dbo.Gender", "ID");
            AddForeignKey("dbo.Category_gender", "Category_ID", "dbo.Category", "ID");
        }
    }
}
