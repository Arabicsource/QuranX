namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreasecolumnlengthsforVerseAnalysisWord : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VerseAnalysisWords", "English", c => c.String(maxLength: 64));
            AlterColumn("dbo.VerseAnalysisWords", "Buckwalter", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VerseAnalysisWords", "Buckwalter", c => c.String(maxLength: 32));
            AlterColumn("dbo.VerseAnalysisWords", "English", c => c.String(maxLength: 32));
        }
    }
}
