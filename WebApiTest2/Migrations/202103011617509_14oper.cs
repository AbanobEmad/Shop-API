namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Stuts_AR", c => c.String());
            AddColumn("dbo.Orders", "Stuts_EN", c => c.String());
            AddColumn("dbo.Orders", "Shipping", c => c.Single(nullable: false));
            AddColumn("dbo.Orders", "PA", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "Send");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Send", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "Date");
            DropColumn("dbo.Orders", "PA");
            DropColumn("dbo.Orders", "Shipping");
            DropColumn("dbo.Orders", "Stuts_EN");
            DropColumn("dbo.Orders", "Stuts_AR");
        }
    }
}
