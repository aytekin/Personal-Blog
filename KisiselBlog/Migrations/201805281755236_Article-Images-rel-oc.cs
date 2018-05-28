namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleImagesreloc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ArticleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "ArticleID");
            AddForeignKey("dbo.Images", "ArticleID", "dbo.Articles", "ArticleID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Images", new[] { "ArticleID" });
            DropColumn("dbo.Images", "ArticleID");
        }
    }
}
