namespace EcommerceCore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnIsShowHomePage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "OrderDisplay", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "IsShowHomePage", c => c.Boolean(nullable: false));
            DropColumn("dbo.Categories", "SiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "SiteId", c => c.Guid());
            DropColumn("dbo.Categories", "IsShowHomePage");
            DropColumn("dbo.Categories", "OrderDisplay");
        }
    }
}
