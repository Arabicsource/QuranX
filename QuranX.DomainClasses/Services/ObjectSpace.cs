﻿using QuranX.DomainClasses.Model;
using System.Data.Entity;

namespace QuranX.DomainClasses.Services
{
    public class ObjectSpace : DbContext
    {
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<VerseText> VerseTexts { get; set; }
        public DbSet<Commentator> Commentators { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
        public DbSet<HadithCollector> HadithCollectors { get; set; }
        public DbSet<HadithReferenceDefinition> HadithReferenceDefinitions { get; set; }
        public DbSet<Hadith> Hadiths { get; set; }
        public DbSet<VerseAnalysisWord> VerseAnalysisWords { get; set; }

        public ObjectSpace() : base("QuranX") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hadith>()
                .HasMany(x => x.References)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<Hadith>()
                .HasMany(x => x.VerseReferences)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<VerseAnalysisWord>()
                .HasMany(x => x.Parts)
                .WithOptional()
                .WillCascadeOnDelete();
            modelBuilder.Entity<VerseAnalysisWordPart>()
                .HasMany(x => x.Decorators)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}
