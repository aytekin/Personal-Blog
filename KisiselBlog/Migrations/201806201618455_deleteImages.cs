namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Images", new[] { "ArticleID" });
            AddColumn("dbo.Articles", "PhotoPath", c => c.String(nullable: false, maxLength: 180));
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(nullable: false, maxLength: 250),
                        ArticleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID);
            
            DropColumn("dbo.Articles", "PhotoPath");
            CreateIndex("dbo.Images", "ArticleID");
            AddForeignKey("dbo.Images", "ArticleID", "dbo.Articles", "ArticleID", cascadeDelete: true);
        }
    }
}
