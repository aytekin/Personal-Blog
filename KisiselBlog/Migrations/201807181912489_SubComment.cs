namespace KisiselBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubComment",
                c => new
                    {
                        SubCommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 400),
                        Name = c.String(nullable: false, maxLength: 15),
                        Surname = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 15),
                        NickName = c.String(maxLength: 15),
                        CommentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCommentID)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .Index(t => t.CommentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubComment", "CommentID", "dbo.Comments");
            DropIndex("dbo.SubComment", new[] { "CommentID" });
            DropTable("dbo.SubComment");
        }
    }
}
