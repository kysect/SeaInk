using System;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Dtos;

public record StudyStudentGroupDto(Guid Id, string GroupName);

public static class StudyStudentGroupDtoExtensions
{
    public static StudyStudentGroupDto ToDto(this StudyStudentGroup ssg)
        => new StudyStudentGroupDto(ssg.Id, ssg.StudentGroup.Name);
}