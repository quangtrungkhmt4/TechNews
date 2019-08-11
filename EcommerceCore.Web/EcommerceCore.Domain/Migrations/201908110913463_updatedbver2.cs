namespace EcommerceCore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedbver2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.String(),
                        IsComment = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        TagID = c.Guid(),
                        MediaID = c.Guid(),
                        ThematicID = c.Guid(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MediaLibraries", t => t.MediaID)
                .ForeignKey("dbo.Tags", t => t.TagID)
                .ForeignKey("dbo.Thematics", t => t.ThematicID)
                .Index(t => t.TagID)
                .Index(t => t.MediaID)
                .Index(t => t.ThematicID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.String(),
                        ParentID = c.Guid(),
                        ArticleID = c.Guid(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleID)
                .Index(t => t.ArticleID);
            
            CreateTable(
                "dbo.MediaLibraries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Path = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Logo = c.String(),
                        Favicon = c.String(),
                        Company = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Hotline = c.String(),
                        Map = c.String(),
                        FanpageFacebook = c.String(),
                        MediaLibraryID = c.Guid(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MediaLibraries", t => t.MediaLibraryID)
                .Index(t => t.MediaLibraryID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Thematics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ParentId = c.Guid(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "ThematicID", "dbo.Thematics");
            DropForeignKey("dbo.Articles", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Articles", "MediaID", "dbo.MediaLibraries");
            DropForeignKey("dbo.Resources", "MediaLibraryID", "dbo.MediaLibraries");
            DropForeignKey("dbo.Comments", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Resources", new[] { "MediaLibraryID" });
            DropIndex("dbo.Comments", new[] { "ArticleID" });
            DropIndex("dbo.Articles", new[] { "ThematicID" });
            DropIndex("dbo.Articles", new[] { "MediaID" });
            DropIndex("dbo.Articles", new[] { "TagID" });
            DropTable("dbo.Thematics");
            DropTable("dbo.Tags");
            DropTable("dbo.Resources");
            DropTable("dbo.MediaLibraries");
            DropTable("dbo.Comments");
            DropTable("dbo.Articles");
        }
    }
}
