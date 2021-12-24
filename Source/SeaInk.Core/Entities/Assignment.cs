using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class Assignment : IEqualityComparer<Assignment>
    {
        public Assignment(
            int universityId,
            string title,
            bool isMilestone,
            DateTime startDate,
            DateTime endDate,
            double minPoints,
            double maxPoints)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            Title = title.ThrowIfNull();
            IsMilestone = isMilestone;
            StartDate = startDate;
            EndDate = endDate;
            MinPoints = minPoints;
            MaxPoints = maxPoints;
        }

#pragma warning disable CS8618
        protected Assignment() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string Title { get; private init; }
        public bool IsMilestone { get; private init; }

        public DateTime StartDate { get; private init; }
        public DateTime EndDate { get; private init; }

        public double MinPoints { get; private init; }
        public double MaxPoints { get; private init; }

        public bool Equals(Assignment? x, Assignment? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(Assignment obj)
            => obj.Id.GetHashCode();
    }
}