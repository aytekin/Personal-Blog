namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleUserRel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "UserID");
            AddForeignKey("dbo.Articles", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserID", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "UserID" });
            DropColumn("dbo.Articles", "UserID");
        }
    }
}
