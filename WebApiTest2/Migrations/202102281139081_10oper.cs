namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10oper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataUsers", "Name_seconde", c => c.String());
            AddColumn("dbo.DataUsers", "Address", c => c.String());
            AddColumn("dbo.DataUsers", "Phone_Number", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataUsers", "Phone_Number");
            DropColumn("dbo.DataUsers", "Address");
            DropColumn("dbo.DataUsers", "Name_seconde");
        }
    }
}
