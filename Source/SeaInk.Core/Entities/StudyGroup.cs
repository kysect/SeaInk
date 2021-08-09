using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entities
{
    public class StudyGroup: IUniversityEntity
    {
        [Key]
        public int Id { get; init; }

        public int UniversityId { get; init; }

        public string Name { get; set; }

        public virtual Student Admin { get; set; }
        public virtual List<Student> Students { get; set; } = new();
    }
}