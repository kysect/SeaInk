using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entities
{
    public class Division: IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string SpreadsheetId { get; set; }
        public virtual List<StudyGroupSubject> StudyGroupSubjects { get; set; } = new();
    }
}