using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuranX.DomainClasses.Models
{
    public class Chapter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }
        [Required, MaxLength(64)]
        public string ArabicName { get; set; }
        [Required, MaxLength(64)]
        public string EnglishName { get; set; }
        public int ChronologicalOrder { get; set; }
        public int NumberOfVerses { get; set; }

        public Chapter() { }
        public Chapter(
            int number,
            string arabicName,
            string englishName,
            int chronologicalOrder,
            int numberOfVerses)
        {
            this.Number = number;
            this.ArabicName = arabicName;
            this.EnglishName = englishName;
            this.ChronologicalOrder = chronologicalOrder;
            this.NumberOfVerses = numberOfVerses;
        }
    }
}
