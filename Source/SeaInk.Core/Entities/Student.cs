using Newtonsoft.Json;

namespace SeaInk.Core.Entities
{
    public class Student : UniversitySystemUser
    {
        [JsonIgnore]
        public StudyGroup Group { get; set; }

        public Student()
        {
            
        }
        public Student(int systemId, StudyGroup group, string token,
            string firstName, string lastName, string midName)
            : base(systemId, token, firstName, lastName, midName)
        {
            Group = group;
        }
    }
}