using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Mentor : User
    {
        public virtual List<StudyGroupSubject> StudyGroupSubjects { get; set; } = new();
    }
}