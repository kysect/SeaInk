namespace SeaInk.Core.Entity
{
    public class Student: Base.User
    {
        public UniversityGroup Group { get; set; }
    }
}