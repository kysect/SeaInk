using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudyStudentGroup : IEquatable<StudyStudentGroup>
    {
        private readonly List<Mentor> _mentors = new List<Mentor>();

        public StudyStudentGroup(StudentGroup studentGroup)
        {
            Id = Guid.NewGuid();
            StudentGroup = studentGroup.ThrowIfNull();
        }

#pragma warning disable CS8618
        private StudyStudentGroup() { }
#pragma warning restore CS8618

        public Guid Id { get; private init; }
        public int? SheetId { get; set; }

        public SubjectDivision? Division { get; internal set; }

        public StudentGroup StudentGroup { get; private init; }

        [NotMapped]
        public IReadOnlyCollection<Mentor> Mentors => _mentors;

        public void AddMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull();

            if (mentors.Any(m => _mentors.Contains(m)))
                throw new ContainingMentorsException(this);

            _mentors.AddRange(mentors);
        }

        public void RemoveMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull();

            if (mentors.Any(m => !_mentors.Contains(m)))
                throw new NotContainingMentorsException(this);

            foreach (Mentor mentor in mentors)
            {
                _mentors.Remove(mentor);
            }
        }

        public bool Equals(StudyStudentGroup? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyStudentGroup);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}