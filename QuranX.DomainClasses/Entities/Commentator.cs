using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Entities
{
    public class Commentator
    {
        [Key]
        [MaxLength(16)]
        public string Code { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }

        public Commentator() { }
        public Commentator(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
