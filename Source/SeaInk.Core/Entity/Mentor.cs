using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeaInk.Core.Entity
{
    public class Mentor : UniversitySystemUser
    {
        public List<Division> Divisions { get; set; }

        public Mentor(int systemId, string token,
            string firstName, string lastName, string midName)
            : base(systemId, token, firstName, lastName, midName)
        {
        }
    }
}