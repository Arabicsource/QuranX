using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Model
{
    public class Translator
    {
        [Key]
        [MaxLength(16)]
        public string Code { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public Translator() { }
        public Translator(string code, string name, int displayOrder)
        {
            this.Code = code;
            this.Name = name;
            this.DisplayOrder = displayOrder;
        }
    }
}
