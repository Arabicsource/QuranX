namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Translatorsandverses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Translators",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 16),
                        Name = c.String(maxLength: 32),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.VerseTexts",
                c => new
                    {
                        Chapter = c.Int(nullable: false),
                        Verse = c.Int(nullable: false),
                        TranslatorCode = c.String(nullable: false, maxLength: 16),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Chapter, t.Verse, t.TranslatorCode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VerseTexts");
            DropTable("dbo.Translators");
        }
    }
}
