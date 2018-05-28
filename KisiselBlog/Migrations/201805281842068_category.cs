namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategoriesArticles",
                c => new
                    {
                        Categories_CategoryID = c.Int(nullable: false),
                        Articles_ArticleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Categories_CategoryID, t.Articles_ArticleID })
                .ForeignKey("dbo.Categories", t => t.Categories_CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Articles_ArticleID, cascadeDelete: true)
                .Index(t => t.Categories_CategoryID)
                .Index(t => t.Articles_ArticleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoriesArticles", "Articles_ArticleID", "dbo.Articles");
            DropForeignKey("dbo.CategoriesArticles", "Categories_CategoryID", "dbo.Categories");
            DropIndex("dbo.CategoriesArticles", new[] { "Articles_ArticleID" });
            DropIndex("dbo.CategoriesArticles", new[] { "Categories_CategoryID" });
            DropTable("dbo.CategoriesArticles");
            DropTable("dbo.Categories");
        }
    }
}
