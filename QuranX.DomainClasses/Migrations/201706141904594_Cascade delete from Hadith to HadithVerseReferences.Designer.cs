// <auto-generated />
namespace QuranX.DomainClasses.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class CascadedeletefromHadithtoHadithVerseReferences : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(CascadedeletefromHadithtoHadithVerseReferences));
        
        string IMigrationMetadata.Id
        {
            get { return "201706141904594_Cascade delete from Hadith to HadithVerseReferences"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}