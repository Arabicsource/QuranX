﻿using QuranX.DomainClasses.Model;
using System.Data.Entity;

namespace QuranX.DomainClasses.ServicesImpl
{
    public class ObjectSpace : DbContext
    {
        public DbSet<Translator> Translators { get; set; }
        public DbSet<VerseText> VerseTexts { get; set; }
        public DbSet<Commentator> Commentators { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
        public DbSet<HadithCollector> HadithCollectors { get; set; }
        public DbSet<HadithReferenceDefinition> HadithReferenceDefinitions { get; set; }

        public ObjectSpace() : base("QuranX") { }
    }
}
