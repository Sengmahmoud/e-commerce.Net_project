namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                  "dbo.CartProducts",
                  c => new
                  {
                      CartId = c.Int(nullable: false),
                      ProductId = c.Int(nullable: false),
                      qnt=c.Int(nullable:false,defaultValue:0)
                  })
                  .PrimaryKey(t => new { t.CartId, t.ProductId })
                  .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                  .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                  .Index(t => t.CartId)
                  .Index(t => t.ProductId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartProducts", "CartId", "dbo.Carts");
            DropIndex("dbo.CartProducts", new[] { "ProductId" });
            DropIndex("dbo.CartProducts", new[] { "CartId" });
            DropTable("dbo.CartProducts");
        }
    }
}
