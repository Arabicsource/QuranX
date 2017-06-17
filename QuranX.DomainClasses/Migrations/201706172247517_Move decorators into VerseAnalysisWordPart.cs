namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedecoratorsintoVerseAnalysisWordPart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VerseAnalysisWordPartDecorators", "VerseAnalysisWordPart_Id", "dbo.VerseAnalysisWordParts");
            DropIndex("dbo.VerseAnalysisWordPartDecorators", new[] { "VerseAnalysisWordPart_Id" });
            AddColumn("dbo.VerseAnalysisWordParts", "Decorators", c => c.String());
            DropTable("dbo.VerseAnalysisWordPartDecorators");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VerseAnalysisWordPartDecorators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Decorator = c.String(maxLength: 32),
                        VerseAnalysisWordPart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.VerseAnalysisWordParts", "Decorators");
            CreateIndex("dbo.VerseAnalysisWordPartDecorators", "VerseAnalysisWordPart_Id");
            AddForeignKey("dbo.VerseAnalysisWordPartDecorators", "VerseAnalysisWordPart_Id", "dbo.VerseAnalysisWordParts", "Id", cascadeDelete: true);
        }
    }
}
