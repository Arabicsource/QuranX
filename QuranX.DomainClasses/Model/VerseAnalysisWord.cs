using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuranX.DomainClasses.Model
{
    public class VerseAnalysisWord
    {
        public int Id { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        public int Index { get; set; }
        [MaxLength(64)]
        public string English { get; set; }
        [MaxLength(64)]
        public string Buckwalter { get; set; }
        public virtual ICollection<VerseAnalysisWordPart> Parts { get; set; }

        public VerseAnalysisWord() { }
        public VerseAnalysisWord(
            int chapter,
            int verse,
            int index,
            string english,
            string buckwalter,
            VerseAnalysisWordPart[] parts)
        {
            this.Chapter = chapter;
            this.Verse = verse;
            this.Index = index;
            this.English = english;
            this.Buckwalter = buckwalter;
            this.Parts = new List<VerseAnalysisWordPart>(parts ?? new VerseAnalysisWordPart[0]);
        }
    }
}
