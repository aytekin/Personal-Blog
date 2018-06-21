namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aboutPageinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutPage",
                c => new
                    {
                        AboutID = c.Int(nullable: false, identity: true),
                        imagePath = c.String(nullable: false),
                        Header = c.String(nullable: false, maxLength: 25),
                        About = c.String(nullable: false, maxLength: 600),
                    })
                .PrimaryKey(t => t.AboutID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AboutPage");
        }
    }
}
