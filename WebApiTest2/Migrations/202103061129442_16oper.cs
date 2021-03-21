namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16oper : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Product", "Seller_ID");
            AddForeignKey("dbo.Product", "Seller_ID", "dbo.Seller", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Seller_ID", "dbo.Seller");
            DropIndex("dbo.Product", new[] { "Seller_ID" });
        }
    }
}
