namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtiComntreloc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ArticleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ArticleID");
            AddForeignKey("dbo.Comments", "ArticleID", "dbo.Articles", "ArticleID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleID" });
            DropColumn("dbo.Comments", "ArticleID");
        }
    }
}
