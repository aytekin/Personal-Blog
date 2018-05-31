namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datesoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        DateID = c.Int(nullable: false, identity: true),
                        DateName = c.String(maxLength: 15),
                        Date = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DateID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 64));
            DropColumn("dbo.Users", "LastLoginDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastLoginDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Dates", "UserID", "dbo.Users");
            DropIndex("dbo.Dates", new[] { "UserID" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 32));
            DropTable("dbo.Dates");
        }
    }
}
