namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeChapterRepositoryinmemory : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Chapters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        Number = c.Int(nullable: false),
                        ArabicName = c.String(nullable: false, maxLength: 64),
                        EnglishName = c.String(nullable: false, maxLength: 64),
                        ChronologicalOrder = c.Int(nullable: false),
                        NumberOfVerses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
        }
    }
}
