namespace SeaInk.Infrastructure.Dto
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
}