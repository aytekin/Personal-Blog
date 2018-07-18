namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ctegorynm64 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
