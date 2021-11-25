using System;

namespace SeaInk.Application.UniversityEntityModels
{
    public record UniversityStudyAssignmentModel(
        int Id,
        string Title,
        bool IsMilestone,
        DateTime StartDate,
        DateTime EndDate,
        double MinPoints,
        double MaxPoints);
}