using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entities
{
    public class Division: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string SpreadsheetId { get; set; }
        public virtual Mentor Mentor { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual List<StudyGroup> Groups { get; set; } = new();
    }
}