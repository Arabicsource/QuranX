using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Entities
{
    public class VerseAnalysisWordPart
    {
        public int Id { get; set; }
        public int Index { get; set; }
        [MaxLength(32)]
        public string Type { get; set; }
        [MaxLength(8)]
        public string Root { get; set; }
        public string Decorators { get; set; }

        public VerseAnalysisWordPart() { }
        public VerseAnalysisWordPart(
            int index,
            string type,
            string root,
            string decorators)
        {
            this.Index = index;
            this.Type = type;
            this.Root = root;
            this.Decorators = decorators;
        }
    }
}