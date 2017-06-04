namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Commentatorsandcommentaries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commentaries",
                c => new
                    {
                        Chapter = c.Int(nullable: false),
                        FirstVerse = c.Int(nullable: false),
                        CommentatorCode = c.String(nullable: false, maxLength: 16),
                        LastVerse = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Chapter, t.FirstVerse, t.CommentatorCode });
            
            CreateTable(
                "dbo.Commentators",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 16),
                        Name = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Commentators");
            DropTable("dbo.Commentaries");
        }
    }
}
