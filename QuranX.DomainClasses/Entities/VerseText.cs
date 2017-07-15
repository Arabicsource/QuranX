using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuranX.DomainClasses.Entities
{
    public class VerseText
    {
        [Key, Column(Order = 0)]
        public int Chapter { get; set; }
        [Key, Column(Order = 1)]
        public int Verse { get; set; }
        [Key, Column(Order = 2)]
        [MaxLength(16)]
        public string TranslatorCode { get; set; }
        public string Text { get; set; }

        public VerseText() { }
        public VerseText(int chapter, int verse, string translatorCode, string text)
        {
            this.Chapter = chapter;
            this.Verse = verse;
            this.TranslatorCode = translatorCode;
            this.Text = text;
        }
    }
}
