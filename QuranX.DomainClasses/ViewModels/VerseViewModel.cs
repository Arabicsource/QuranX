using QuranX.DomainClasses.Models;

namespace QuranX.DomainClasses.ViewModels
{
    public class VerseViewModel
    {
        public int TafsirCount { get; private set; }
        public int HadithCount { get; private set; }
        public int RootWordCount { get; private set; }
        public Chapter Chapter { get; private set; }
        public VerseText[] Translations { get; private set; }

    }
}
