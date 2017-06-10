namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hadithversereferences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HadithVerseReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chapter = c.Int(nullable: false),
                        FirstVerse = c.Int(nullable: false),
                        LastVerse = c.Int(nullable: false),
                        Hadith_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hadiths", t => t.Hadith_Id)
                .Index(t => t.Hadith_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HadithVerseReferences", "Hadith_Id", "dbo.Hadiths");
            DropIndex("dbo.HadithVerseReferences", new[] { "Hadith_Id" });
            DropTable("dbo.HadithVerseReferences");
        }
    }
}
