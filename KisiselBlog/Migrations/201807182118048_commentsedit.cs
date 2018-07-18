namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.Comments", "UserSurname", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.Comments", "UserEmail", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.Comments", "UserPhoto", c => c.String());
            AddColumn("dbo.SubComment", "UserName", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.SubComment", "UserSurname", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.SubComment", "UserEmail", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.SubComment", "UserPhoto", c => c.String());
            DropColumn("dbo.Comments", "UserNickname");
            DropColumn("dbo.SubComment", "UserNickname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubComment", "UserNickname", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Comments", "UserNickname", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.SubComment", "UserPhoto");
            DropColumn("dbo.SubComment", "UserEmail");
            DropColumn("dbo.SubComment", "UserSurname");
            DropColumn("dbo.SubComment", "UserName");
            DropColumn("dbo.Comments", "UserPhoto");
            DropColumn("dbo.Comments", "UserEmail");
            DropColumn("dbo.Comments", "UserSurname");
            DropColumn("dbo.Comments", "UserName");
        }
    }
}
