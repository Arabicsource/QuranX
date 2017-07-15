using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Entities
{
    public class HadithCollector
    {
        [Key]
        [MaxLength(16)]
        public string Code { get; set; }
        public string Name { get; set; }

        public HadithCollector() { }
        public HadithCollector(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
