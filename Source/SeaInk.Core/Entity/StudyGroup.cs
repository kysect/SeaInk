using System.Collections.Generic;

namespace SeaInk.Core.Entity
{
    public class StudyGroup
    {
        public int SystemId { get; set; }
        public string Name { get; set; }
        public int AdminSystemId { get; set; }
        public new List<Student> Students { get; set; }

        public StudyGroup(int id, string name, int adminId, List<Student> students)
        {
            SystemId = id;
            Name = name;
            AdminSystemId = adminId;
            Students = students;
        }
    }
}