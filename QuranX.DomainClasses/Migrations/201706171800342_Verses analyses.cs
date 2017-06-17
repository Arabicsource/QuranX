namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versesanalyses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VerseAnalysisWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chapter = c.Int(nullable: false),
                        Verse = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        English = c.String(maxLength: 32),
                        Buckwalter = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VerseAnalysisWordParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Type = c.String(maxLength: 32),
                        Root = c.String(maxLength: 8),
                        VerseAnalysisWord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VerseAnalysisWords", t => t.VerseAnalysisWord_Id, cascadeDelete: true)
                .Index(t => t.VerseAnalysisWord_Id);
            
            CreateTable(
                "dbo.VerseAnalysisWordPartDecorators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Decorator = c.String(maxLength: 32),
                        VerseAnalysisWordPart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VerseAnalysisWordParts", t => t.VerseAnalysisWordPart_Id, cascadeDelete: true)
                .Index(t => t.VerseAnalysisWordPart_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VerseAnalysisWordParts", "VerseAnalysisWord_Id", "dbo.VerseAnalysisWords");
            DropForeignKey("dbo.VerseAnalysisWordPartDecorators", "VerseAnalysisWordPart_Id", "dbo.VerseAnalysisWordParts");
            DropIndex("dbo.VerseAnalysisWordPartDecorators", new[] { "VerseAnalysisWordPart_Id" });
            DropIndex("dbo.VerseAnalysisWordParts", new[] { "VerseAnalysisWord_Id" });
            DropTable("dbo.VerseAnalysisWordPartDecorators");
            DropTable("dbo.VerseAnalysisWordParts");
            DropTable("dbo.VerseAnalysisWords");
        }
    }
}
