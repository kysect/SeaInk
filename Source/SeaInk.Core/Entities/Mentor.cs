using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class Mentor : UniversitySystemUser
    {
        public List<Division> Divisions { get; set; } = new List<Division>();

        public Mentor()
            : base() { }

        public Mentor(int systemId, string token,
            string firstName, string lastName, string midName)
            : base(systemId, token, firstName, lastName, midName) { }
    }
}