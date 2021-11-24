using System;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudyAssignment : IEquatable<StudyAssignment>
    {
        public StudyAssignment(
            int universityId,
            string title,
            bool isMilestone,
            DateTime startDate,
            DateTime endDate,
            float minPoints,
            float maxPoints)
        {
            UniversityId = universityId;
            Title = title.ThrowIfNull(nameof(title));
            IsMilestone = isMilestone;
            StartDate = startDate;
            EndDate = endDate;
            MinPoints = minPoints;
            MaxPoints = maxPoints;
        }

        [Key]
        public int Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Title { get; private init; }
        public bool IsMilestone { get; private init; }

        public DateTime StartDate { get; private init; }
        public DateTime EndDate { get; private init; }

        public double MinPoints { get; private init; }
        public double MaxPoints { get; private init; }

        public bool Equals(StudyAssignment? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyAssignment);

        public override int GetHashCode()
            => Id;
    }
}