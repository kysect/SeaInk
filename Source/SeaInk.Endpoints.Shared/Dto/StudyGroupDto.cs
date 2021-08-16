using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record StudyGroupDto(int Id, string Name, StudentDto Admin, IEnumerable<StudentDto> Students);
    
    public static class StudyGroupExtension
    {
        public static StudyGroupDto ToDto(this StudyGroup group)
        {
            return new StudyGroupDto(group.Id, group.Name, group.Admin.ToDto(),
                group.Students.Select(student => student.ToDto()).ToList());
        }
    }
}
