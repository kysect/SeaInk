using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class StudyGroup
    {
        public int SystemId { get; set; }
        public string Name { get; set; }
        public Student Admin { get; set; }
        public List<Student> Students { get; set; }

        public StudyGroup()
        {
            SystemId = -1;
            Students = new List<Student>();
        }
        public StudyGroup(int id, string name, Student admin, List<Student> students)
        {
            SystemId = id;
            Name = name;
            Admin = admin;
            Students = students;
        }
    }
}