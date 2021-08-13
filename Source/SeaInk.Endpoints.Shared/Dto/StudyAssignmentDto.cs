using System;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Shared.Dto
{
    public record StudyAssignmentDto(int Id, string Title, bool IsMilestone, DateTime StartDate, DateTime EndDate,
        float MinPoints, float MaxPoints);

    public static class StudyAssignmentExtension
    {
        public static StudyAssignmentDto ToDto(this StudyAssignment sa)
        {
            return new StudyAssignmentDto(sa.Id, sa.Title, sa.IsMilestone, sa.StartDate, sa.EndDate,
                sa.MinPoints, sa.MaxPoints);
        }
    }
}