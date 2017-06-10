namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcascadedeletefromHadithtoHadithReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HadithReferences", "Hadith_Id", "dbo.Hadiths");
            AddForeignKey("dbo.HadithReferences", "Hadith_Id", "dbo.Hadiths", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HadithReferences", "Hadith_Id", "dbo.Hadiths");
            AddForeignKey("dbo.HadithReferences", "Hadith_Id", "dbo.Hadiths", "Id");
        }
    }
}
