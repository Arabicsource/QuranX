using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuranX.DomainClasses.Model
{
    public class HadithReferenceDefinition
    {
        [Key, Column(Order = 0)]
        [MaxLength(16)]
        public string CollectorCode { get; set; }
        [Key, Column(Order = 1)]
        [MaxLength(16)]
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        [Required, MaxLength(16)]
        public string KeyPart1Name { get; set; }
        [MaxLength(16)]
        public string KeyPart2Name { get; set; }
        [MaxLength(16)]
        public string KeyPart3Name { get; set; }
        [MaxLength(32)]
        public string ValuePrefix { get; set; }

        public HadithReferenceDefinition() { }
        public HadithReferenceDefinition(
            string collectorCode,
            string code,
            string name,
            bool isPrimary,
            string keyPart1Name,
            string keyPart2Name,
            string keyPart3Name,
            string valuePrefix)
        {
            this.CollectorCode = collectorCode;
            this.Code = code;
            this.Name = name;
            this.IsPrimary = isPrimary;
            this.KeyPart1Name = keyPart1Name;
            this.KeyPart2Name = keyPart2Name;
            this.KeyPart3Name = keyPart3Name;
            this.ValuePrefix = valuePrefix;
        }
            
    }
}
