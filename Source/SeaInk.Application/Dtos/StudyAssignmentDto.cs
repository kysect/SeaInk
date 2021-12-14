using System;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Dtos
{
    public record StudyAssignmentDto(
        Guid Id,
        int UniversityId,
        string Title,
        bool IsMilestone,
        DateTime StartDate,
        DateTime EndDate,
        double MinPoints,
        double MaxPoints);

    public static class StudyAssignmentDtoExtension
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
    }
}