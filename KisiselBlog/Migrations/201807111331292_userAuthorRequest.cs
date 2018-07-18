namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAuthorRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "authorRequest", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "authorRequest");
        }
    }
}
