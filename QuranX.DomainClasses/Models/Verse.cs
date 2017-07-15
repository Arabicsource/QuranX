using QuranX.DomainClasses.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuranX.DomainClasses.Models
{
    public class Verse
    {
        public Chapter Chapter { get; set; }
        public int VerseNumber { get; set; }
        public VerseTranslation[] Translations { get; set; }
        public int TafsirCount { get; set; }
        public int HadithCount { get; set; }
        public int RootWordCount { get; set; }

        public Verse(
            Chapter chapter, 
            int verseNumber, 
            IEnumerable<VerseTranslation> translations, 
            int tafsirCount, 
            int hadithCount, 
            int rootWordCount)
        {
            Chapter = chapter;
            VerseNumber = verseNumber;
            Translations = (translations ?? new VerseTranslation[0]).ToArray();
            TafsirCount = tafsirCount;
            HadithCount = hadithCount;
            RootWordCount = rootWordCount;
        }


    }
}
