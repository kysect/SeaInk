using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudyGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public StudentDto Admin { get; set; }
        
        public IReadonlyList<StudentDto> Students { get; }
        
        public StudyGroupDto()
        {
            Id = -1;
        }

        public StudyGroupDto(StudyGroup group)
        {
            Id = group.Id;
            Name = group.Name;
            Admin = new StudentDto(group.Admin);
            Students = group.Students.Select(student => new StudentDto(student)).ToList();
        }
    }
}
