namespace QuranX.DomainClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addhadithcollectorsreferencedefinitions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HadithCollectors",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 16),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.HadithReferenceDefinitions",
                c => new
                    {
                        CollectorCode = c.String(nullable: false, maxLength: 16),
                        Code = c.String(nullable: false, maxLength: 16),
                        Name = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        KeyPart1Name = c.String(nullable: false, maxLength: 16),
                        KeyPart2Name = c.String(maxLength: 16),
                        KeyPart3Name = c.String(maxLength: 16),
                        ValuePrefix = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => new { t.CollectorCode, t.Code });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HadithReferenceDefinitions");
            DropTable("dbo.HadithCollectors");
        }
    }
}
