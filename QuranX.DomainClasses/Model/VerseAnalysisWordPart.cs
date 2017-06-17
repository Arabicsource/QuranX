using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Model
{
    public class VerseAnalysisWordPart
    {
        public int Id { get; set; }
        public int Index { get; set; }
        [MaxLength(32)]
        public string Type { get; set; }
        [MaxLength(8)]
        public string Root { get; set; }
        public virtual ICollection<VerseAnalysisWordPartDecorator> Decorators { get; set; }

        public VerseAnalysisWordPart() { }
        public VerseAnalysisWordPart(
            int index,
            string type,
            string root,
            VerseAnalysisWordPartDecorator[] decorators)
        {
            this.Index = index;
            this.Type = type;
            this.Root = root;
            this.Decorators = new List<VerseAnalysisWordPartDecorator>(
                decorators ?? new VerseAnalysisWordPartDecorator[0]);
        }
    }
}