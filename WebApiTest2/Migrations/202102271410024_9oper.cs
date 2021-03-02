namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Shipping", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Shipping");
        }
    }
}
