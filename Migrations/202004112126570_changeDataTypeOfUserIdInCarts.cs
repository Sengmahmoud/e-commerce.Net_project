namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDataTypeOfUserIdInCarts : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Carts", new[] { "User_Id" });
            DropColumn("dbo.Carts", "UserId");
            RenameColumn(table: "dbo.Carts", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Carts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Carts", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Carts", new[] { "UserId" });
            AlterColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "User_Id");
        }
    }
}
