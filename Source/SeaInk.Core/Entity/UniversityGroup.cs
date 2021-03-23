
using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class UniversityGroup: Base.Group
    {
        public int HeadmanIdentifier { get; set; }
        public new List<Student> Members { get; set; }
    }
}