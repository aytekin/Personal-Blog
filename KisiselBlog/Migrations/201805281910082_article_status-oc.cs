namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class article_statusoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "status");
        }
    }
}
