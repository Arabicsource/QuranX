namespace QuranX.DomainClasses.Entities
{
    public class HadithVerseReference
    {
        public int Id { get; set; }
        public int Chapter { get; set; }
        public int FirstVerse { get; set; }
        public int LastVerse { get; set; }

        public HadithVerseReference() { }
        public HadithVerseReference(int chapter, int firstVerse, int lastVerse)
        {
            this.Chapter = chapter;
            this.FirstVerse = firstVerse;
            this.LastVerse = lastVerse;
        }
    }
}
