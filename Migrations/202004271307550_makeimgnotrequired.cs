namespace e_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeimgnotrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "img", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "img", c => c.String(nullable: false));
        }
    }
}
