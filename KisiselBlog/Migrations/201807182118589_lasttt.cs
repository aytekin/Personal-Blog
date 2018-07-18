namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lasttt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CommentPPPath", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CommentPPPath");
        }
    }
}
