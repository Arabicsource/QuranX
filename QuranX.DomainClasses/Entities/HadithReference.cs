using System.ComponentModel.DataAnnotations;

namespace QuranX.DomainClasses.Entities
{
    public class HadithReference
    {
        public int Id { get; set; }
        [Required, MaxLength(16)]
        public string Code { get; set; }
        [Required, MaxLength(8)]
        public string Part1 { get; set; }
        [MaxLength(8)]
        public string Part2 { get; set; }
        [MaxLength(8)]
        public string Part3 { get; set; }
        [MaxLength(8)]
        public string Suffix { get; set; }

        public HadithReference() { }
        public HadithReference(string code, string part1, string part2, string part3, string suffix)
        {
            this.Code = code;
            this.Part1 = part1;
            this.Part2 = part2;
            this.Part3 = part3;
            this.Suffix = suffix;
        }
    }
}
