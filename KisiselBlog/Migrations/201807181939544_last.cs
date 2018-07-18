namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 400));
            AddColumn("dbo.Comments", "UserNickname", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.SubComment", "Content", c => c.String(nullable: false, maxLength: 400));
            AddColumn("dbo.SubComment", "UserNickname", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.Comments", "Text");
            DropColumn("dbo.Comments", "Name");
            DropColumn("dbo.Comments", "Surname");
            DropColumn("dbo.Comments", "Email");
            DropColumn("dbo.Comments", "NickName");
            DropColumn("dbo.SubComment", "Text");
            DropColumn("dbo.SubComment", "Name");
            DropColumn("dbo.SubComment", "Surname");
            DropColumn("dbo.SubComment", "Email");
            DropColumn("dbo.SubComment", "NickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubComment", "NickName", c => c.String(maxLength: 15));
            AddColumn("dbo.SubComment", "Email", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.SubComment", "Surname", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.SubComment", "Name", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.SubComment", "Text", c => c.String(nullable: false, maxLength: 400));
            AddColumn("dbo.Comments", "NickName", c => c.String(maxLength: 15));
            AddColumn("dbo.Comments", "Email", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "Surname", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "Name", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 400));
            DropColumn("dbo.SubComment", "UserNickname");
            DropColumn("dbo.SubComment", "Content");
            DropColumn("dbo.Comments", "UserNickname");
            DropColumn("dbo.Comments", "Content");
        }
    }
}
