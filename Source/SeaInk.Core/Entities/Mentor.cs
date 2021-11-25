namespace SeaInk.Core.Entities
{
    public sealed class Mentor : User
    {
        public Mentor(int universityId, string firstName, string lastName, string middleName)
            : base(universityId, firstName, lastName, middleName) { }
    }
}