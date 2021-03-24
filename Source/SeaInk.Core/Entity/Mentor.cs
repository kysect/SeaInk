using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entity
{
    public class Mentor : UniversitySystemUser
    {
        //Айдишники предметов которые ведёт ментор
        public List<int> SystemSubjectIds { get; set; }

        //Dictionary<SystemSubjectId, SystemGroupId> 
        public Dictionary<int, int> Groups { get; set; }

        public Mentor(int systemId, string token,
            string firstName, string lastName, string midName)
            : base(systemId, token, firstName, lastName, midName)
        {
        }
    }
}