using System.Linq;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.Dto;

namespace SeaInk.Application.Extensions;

public static class DtoExtensions
{
    public static StudyAssignmentDto ToDto(this Assignment assignment)
        => new StudyAssignmentDto(
            assignment.Id,
            assignment.UniversityId,
            assignment.Title,
            assignment.IsMilestone,
            assignment.StartDate,
            assignment.EndDate,
            assignment.MinPoints,
            assignment.MaxPoints);

    public static StudyStudentGroupDto ToDto(this StudyStudentGroup ssg)
        => new StudyStudentGroupDto(ssg.Id, ssg.StudentGroup.Name);

    public static SubjectDto ToDto(this Subject subject)
        => new SubjectDto(
            subject.Id,
            subject.UniversityId,
            subject.Name,
            subject.Assignments.Select(a => a.ToDto()).ToList());
}