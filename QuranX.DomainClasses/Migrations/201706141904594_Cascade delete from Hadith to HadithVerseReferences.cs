namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadedeletefromHadithtoHadithVerseReferences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HadithVerseReferences", "Hadith_Id", "dbo.Hadiths");
            AddForeignKey("dbo.HadithVerseReferences", "Hadith_Id", "dbo.Hadiths", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HadithVerseReferences", "Hadith_Id", "dbo.Hadiths");
            AddForeignKey("dbo.HadithVerseReferences", "Hadith_Id", "dbo.Hadiths", "Id");
        }
    }
}
