namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChapters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: false),
                        ArabicName = c.String(nullable: false, maxLength: 64),
                        EnglishName = c.String(nullable: false, maxLength: 64),
                        ChronologicalOrder = c.Int(nullable: false),
                        NumberOfVerses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chapters");
        }
    }
}
