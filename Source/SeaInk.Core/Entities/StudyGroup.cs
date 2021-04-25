using System.Collections.Generic;

namespace SeaInk.Core.Entities
{
    public class StudyGroup
    {
        public int SystemId { get; set; } = -1;
        public string Name { get; set; } = "";
        public Student Admin { get; set; } = new Student();
        public List<Student> Students { get; set; } = new List<Student>();

        public StudyGroup()
        {
            
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