using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Mentor: User
    {
        public virtual List<Division> Divisions { get; set; } = new();
    }
}