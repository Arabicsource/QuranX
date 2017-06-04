using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuranX.DomainClasses.Model
{
    public class Commentary
    {
        [Key, Column(Order = 0)]
        public int Chapter { get; set; }
        [Key, Column(Order = 1)]
        public int FirstVerse { get; set; }
        [Key, Column(Order = 2)]
        [Required, MaxLength(16)]
        public string CommentatorCode { get; set; }
        public int LastVerse { get; set; }
        public string Text { get; set; }

        public Commentary() { }
        public Commentary(int chapter, int firstVerse, int lastVerse, string commentatorCode, string text)
        {
            this.Chapter = chapter;
            this.FirstVerse = firstVerse;
            this.LastVerse = lastVerse;
            this.CommentatorCode = commentatorCode;
            this.Text = text;
        }
    }
}
