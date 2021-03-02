namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1opertion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cash = c.Single(nullable: false),
                        point = c.Long(nullable: false),
                        Name = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DataUsers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.DataUsers", new[] { "UserID" });
            DropTable("dbo.DataUsers");
        }
    }
}
