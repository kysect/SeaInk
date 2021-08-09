using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Division: IEntity
    {
        public int Id { get; }
        public string SpreadsheetId { get; set; }
        public virtual Mentor Mentor { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual List<StudyGroup> Groups { get; set; } = new();
    }
}