using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Model
{
    public class VerseAnalysisWordPartDecorator
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string Decorator { get; set; }

        public VerseAnalysisWordPartDecorator() { }
        public VerseAnalysisWordPartDecorator(string decorator)
        {
            this.Decorator = decorator;
        }
    }
}