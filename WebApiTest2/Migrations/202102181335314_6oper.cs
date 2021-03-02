namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "S_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "S_Id");
            AddForeignKey("dbo.Carts", "S_Id", "dbo.Size", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "S_Id", "dbo.Size");
            DropIndex("dbo.Carts", new[] { "S_Id" });
            DropColumn("dbo.Carts", "S_Id");
        }
    }
}
