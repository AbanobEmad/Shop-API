namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Show_Home", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Show_Home");
        }
    }
}
