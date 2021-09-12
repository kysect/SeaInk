using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SeaInk.Core.Entities
{
    public class StudyGroup: IUniversityEntity
    {
        [Key]
        public int Id { get; set; }

        public int UniversityId { get; init; }

        public string Name { get; set; }

        //Сделал это поле int дабы избежать циклической зависимости
        public int? AdminId { get; set; }

        /// <summary>
        /// Do not remove this attribute. Prevent circular reference
        /// </summary>
        [NotMapped]
        public Student Admin => AdminId is null ? null : Students.Single(s => s.Id == AdminId);

        public virtual List<Student> Students { get; set; } = new();
    }
}