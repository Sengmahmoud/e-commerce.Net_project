namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnotaion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "userid" });
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "img", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "userid", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "userid");
            AddForeignKey("dbo.Orders", "userid", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "userid" });
            AlterColumn("dbo.Orders", "userid", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Products", "img", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            CreateIndex("dbo.Orders", "userid");
            AddForeignKey("dbo.Orders", "userid", "dbo.AspNetUsers", "Id");
        }
    }
}
