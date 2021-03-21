namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Show_Home", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Show_Home");
        }
    }
}
