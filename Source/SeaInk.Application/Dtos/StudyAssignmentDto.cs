using System;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public sealed class StudyAssignmentDto : IEquatable<StudyAssignmentDto>
    {
        public StudyAssignmentDto(
            Guid id,
            int universityId,
            string title,
            bool isMilestone,
            DateTime startDate,
            DateTime endDate,
            double minPoints,
            double maxPoints)
        {
            Id = id;
            UniversityId = universityId;
            Title = title.ThrowIfNull();
            IsMilestone = isMilestone;
            StartDate = startDate;
            EndDate = endDate;
            MinPoints = minPoints;
            MaxPoints = maxPoints;
        }

        public Guid Id { get; }
        public int UniversityId { get; }
        public string Title { get; }
        public bool IsMilestone { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public double MinPoints { get; }
        public double MaxPoints { get; }

        public bool Equals(StudyAssignmentDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyAssignmentDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}