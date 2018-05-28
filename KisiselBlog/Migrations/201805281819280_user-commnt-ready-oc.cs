namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercommntreadyoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Name", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "Surname", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "Email", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "NickName", c => c.String(maxLength: 15));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Text", c => c.String(maxLength: 400));
            DropColumn("dbo.Comments", "NickName");
            DropColumn("dbo.Comments", "Email");
            DropColumn("dbo.Comments", "Surname");
            DropColumn("dbo.Comments", "Name");
        }
    }
}
