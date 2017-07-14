using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Models
{
    public class Hadith
    {
        public int Id { get; set; }
        [Required, MaxLength(16)]
        public string CollectorCode { get; set; }
        public string Arabic { get; set; }
        public string English { get; set; }
        public virtual ICollection<HadithReference> References { get; set; } = new List<HadithReference>();
        public virtual ICollection<HadithVerseReference> VerseReferences { get; set; } = new List<HadithVerseReference>();

        public Hadith() { }
        public Hadith(
            string collectorCode, 
            string arabic, 
            string english, 
            ICollection<HadithReference> references,
            ICollection<HadithVerseReference> verseReferences)
        {
            this.CollectorCode = collectorCode;
            this.Arabic = arabic ?? "";
            this.English = english ?? "";
            this.References = references ?? new List<HadithReference>();
            this.VerseReferences = verseReferences ?? new List<HadithVerseReference>();
        }
    }
}
