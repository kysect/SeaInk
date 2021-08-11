using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeaInk.Core.Entities
{
    public class User: IUniversityEntity
    {
        [Key]
        public int Id { get; set; }

        public int UniversityId { get; init; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";
    }
}