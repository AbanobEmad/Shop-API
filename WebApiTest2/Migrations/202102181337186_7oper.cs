namespace WebApiTest2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7oper : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Carts", name: "S_Id", newName: "Size_Id");
            RenameIndex(table: "dbo.Carts", name: "IX_S_Id", newName: "IX_Size_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Carts", name: "IX_Size_Id", newName: "IX_S_Id");
            RenameColumn(table: "dbo.Carts", name: "Size_Id", newName: "S_Id");
        }
    }
}
