namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQntToOrdersProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "qnt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "qnt");
        }
    }
}
