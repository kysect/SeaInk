using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entity
{
    public class UniversitySystemUser
    {
        [Key] public int SystemId { get; set; }
        public string Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }

        public UniversitySystemUser(int systemId, string token,
            string firstName, string lastName, string midName)
        {
            SystemId = systemId;
            Token = token;
            FirstName = firstName;
            LastName = lastName;
            MidName = midName;
        }
    }
}