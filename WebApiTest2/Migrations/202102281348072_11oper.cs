namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11oper : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        CustomerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.AspNetUsers", "Id");
        }
    }
}
