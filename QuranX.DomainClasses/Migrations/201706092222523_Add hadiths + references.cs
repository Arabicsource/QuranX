namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addhadithsreferences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hadiths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollectorCode = c.String(nullable: false, maxLength: 16),
                        Arabic = c.String(),
                        English = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HadithReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 16),
                        Part1 = c.String(nullable: false, maxLength: 8),
                        Part2 = c.String(maxLength: 8),
                        Part3 = c.String(maxLength: 8),
                        Suffix = c.String(maxLength: 8),
                        Hadith_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hadiths", t => t.Hadith_Id)
                .Index(t => t.Hadith_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HadithReferences", "Hadith_Id", "dbo.Hadiths");
            DropIndex("dbo.HadithReferences", new[] { "Hadith_Id" });
            DropTable("dbo.HadithReferences");
            DropTable("dbo.Hadiths");
        }
    }
}
