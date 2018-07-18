namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AddedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubComment", "AddedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubComment", "AddedDate");
            DropColumn("dbo.Comments", "AddedDate");
        }
    }
}
