namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optionalsoc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Users", "AboutUser", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "AboutUser", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
