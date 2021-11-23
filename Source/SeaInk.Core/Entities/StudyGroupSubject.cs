using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class StudyGroupSubject : IEntity
    {
        public int Id {  get; set; }

        public virtual StudyGroup StudyGroup { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual List<Mentor> Mentors { get; set; }
    }
}