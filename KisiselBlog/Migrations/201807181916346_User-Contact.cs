namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserTwitterAdress", c => c.String());
            AddColumn("dbo.Users", "UserGithubAdress", c => c.String());
            AddColumn("dbo.Users", "UserBitbucketAdress", c => c.String());
            AddColumn("dbo.Users", "UserlinkedinAdress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserlinkedinAdress");
            DropColumn("dbo.Users", "UserBitbucketAdress");
            DropColumn("dbo.Users", "UserGithubAdress");
            DropColumn("dbo.Users", "UserTwitterAdress");
        }
    }
}
