using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public sealed class SubjectDto : IEquatable<SubjectDto>
    {
        public SubjectDto(
            Guid id,
            int universityId,
            string name,
            DateTime startDate,
            DateTime endDate,
            IReadOnlyCollection<StudyAssignmentDto> assignments)
        {
            Id = id;
            UniversityId = universityId;
            Name = name.ThrowIfNull();
            StartDate = startDate;
            EndDate = endDate;
            Assignments = assignments.ThrowIfNull().ToList();
        }

        public Guid Id { get; }
        public int UniversityId { get; }
        public string Name { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public IReadOnlyCollection<StudyAssignmentDto> Assignments { get; }

        public bool Equals(SubjectDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as SubjectDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}