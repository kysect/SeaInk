namespace SeaInk.Core.Entity
{
    public class GroupMentor
    {
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }

        public string SubjectName { get; set; }
        public string GroupName { get; set; }
    }
}