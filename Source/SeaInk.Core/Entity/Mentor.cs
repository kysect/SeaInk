using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entity
{
    public class Mentor
    {
        [Key]
        public int UniversitySystemId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}