namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8oper : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "Size_Id", "dbo.Size");
            DropIndex("dbo.Carts", new[] { "Size_Id" });
            AlterColumn("dbo.Carts", "Size_Id", c => c.Int());
            CreateIndex("dbo.Carts", "Size_Id");
            AddForeignKey("dbo.Carts", "Size_Id", "dbo.Size", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Size_Id", "dbo.Size");
            DropIndex("dbo.Carts", new[] { "Size_Id" });
            AlterColumn("dbo.Carts", "Size_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "Size_Id");
            AddForeignKey("dbo.Carts", "Size_Id", "dbo.Size", "ID", cascadeDelete: true);
        }
    }
}
