using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record StudentDto(
        int Id,
        string FirstName,
        string LastName,
        string MiddleName,
        string FullName,
        int GroupId,
        string GroupName);

    public static class StudentExtension
    {
        public static StudentDto ToDto(this Student student)
        {
            return new StudentDto(student.Id, student.FirstName, student.LastName, student.MiddleName,
                                  student.FullName, student.Group.Id, student.Group.Name);
        }
    }
}