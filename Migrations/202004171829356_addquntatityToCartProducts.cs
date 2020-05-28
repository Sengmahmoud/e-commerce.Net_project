namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addquntatityToCartProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartProducts", "qnt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartProducts", "qnt");
        }
    }
}
