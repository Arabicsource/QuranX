namespace QuranX.DomainClasses.Models
{
    public class VerseRange
    {
        public int Chapter { get; private set; }
        public int FirstVerse { get; private set; }
        public int LastVerse { get; private set; }

        public VerseRange(int chapter, int firstVerse, int lastVerse)
        {
            Chapter = chapter;
            FirstVerse = firstVerse;
            LastVerse = lastVerse;
        }
    }
}
