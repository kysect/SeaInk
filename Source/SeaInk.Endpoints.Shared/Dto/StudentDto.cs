using System.ComponentModel.DataAnnotations;
using SeaInk.Core.Entities;
namespace SeaInk.Endpoints.Shared.Dto
{
    public class StudentDto
    {
        [Key] public int SystemId { get; set; }
        
        public string Token { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MidName { get; set; }

        public string FullName => LastName + " " + FirstName + " " + MidName;

        public int GroupId { get; set; }

        public string GroupName { get; set; }


        public StudentDto()
        {
            SystemId = -1;
            GroupId = -1;
        }

        public StudentDto(Student student)
        {
            SystemId = student.SystemId;
            Token = student.Token;
            FirstName = student.FirstName;
            MidName = student.MidName;
            GroupId = student.Group.SystemId;
            GroupName = student.Group.Name;
        }
    }
}