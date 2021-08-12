namespace SeaInk.Core.Entities
{
    public class Student: User
    {
        public virtual StudyGroup Group { get; set; }
    }
}