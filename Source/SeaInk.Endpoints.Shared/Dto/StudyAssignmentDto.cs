using System;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record StudyAssignmentDto(
        int Id,
        string Title,
        bool IsMilestone,
        DateTime StartDate,
        DateTime EndDate,
        float MinPoints,
        float MaxPoints);

    public static class StudyAssignmentExtension
    {
        public static StudyAssignmentDto ToDto(this StudyAssignment assignment)
        {
            return new StudyAssignmentDto(assignment.Id, assignment.Title, assignment.IsMilestone, assignment.StartDate,
                                          assignment.EndDate, assignment.MinPoints, assignment.MaxPoints);
        }
    }
}