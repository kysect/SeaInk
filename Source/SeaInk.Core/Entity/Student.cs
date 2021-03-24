namespace SeaInk.Core.Entity
{
    public class Student: UniversitySystemUser
    {
        public int SystemGroupId { get; set; }
        
        public Student(int systemId, int systemGroupId,string token,
            string firstName, string lastName, string midName)
        : base(systemId, token, firstName, lastName, midName)
        {
            SystemGroupId = systemGroupId;
        }
    }
}