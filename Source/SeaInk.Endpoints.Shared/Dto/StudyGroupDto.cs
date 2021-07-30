using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudyGroupDto
    {
        public int SystemId { get; set; }
        public string Name { get; set; }
        
        public  StudentDto Admin { get; set; }
        
        public List<StudentDto> Students { get; set; }
        
        public StudyGroupDto()
        {
            SystemId = -1;
        }

        public StudyGroupDto(StudyGroup group)
        {
            SystemId = group.SystemId;
            Name = group.Name;
            Admin = new StudentDto(group.Admin);
            Students = group.Students.Select(student => new StudentDto(student)).ToList();
        }
    }
}