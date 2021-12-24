using System;
using SeaInk.Core.Entities;

namespace SeaInk.Core.UniversityServiceModels
{
    public record AssignmentUniversityModel(
        int UniversityId,
        string Title,
        bool IsMilestone,
        DateTime StartDate,
        DateTime EndDate,
        double MinPoints,
        double MaxPoints);

    public static class AssignmentUniversityModelExtensions
    {
        public static Assignment ToAssignment(this AssignmentUniversityModel model)
        {
            return new Assignment(
                model.UniversityId,
                model.Title,
                model.IsMilestone,
                model.StartDate,
                model.EndDate,
                model.MinPoints,
                model.MaxPoints);
        }
    }
}