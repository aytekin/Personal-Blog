namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userstats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ControlCode", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "ControlCodeStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ControlCodeStatus");
            DropColumn("dbo.Users", "ControlCode");
        }
    }
}
