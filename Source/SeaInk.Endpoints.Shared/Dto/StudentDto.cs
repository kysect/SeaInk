using System.ComponentModel.DataAnnotations;
using SeaInk.Core.Entities;
namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudentDto
    {
        [Key] public int Id { get; set; }
        
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }

        public string FullName => LastName + " " + FirstName + " " + MiddleName;

        public int GroupId { get; set; }

        public string GroupName { get; set; }


        public StudentDto()
        {
            Id = -1;
            GroupId = -1;
        }

        public StudentDto(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            MiddleName = student.MiddleName;
            GroupId = student.Group.Id;
            GroupName = student.Group.Name;
        }
    }
}