using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class StudyStudentGroup : IEqualityComparer<StudyStudentGroup>
    {
        private readonly List<Mentor> _mentors = new List<Mentor>();

        public StudyStudentGroup(StudentGroup studentGroup)
        {
            Id = Guid.NewGuid();
            StudentGroup = studentGroup.ThrowIfNull();
        }

#pragma warning disable CS8618
        protected StudyStudentGroup() { }
#pragma warning restore CS8618

        public Guid Id { get; private init; }
        public int? SheetId { get; set; }

        public virtual SubjectDivision? Division { get; internal set; }

        public virtual StudentGroup StudentGroup { get; private init; }

        public virtual IReadOnlyCollection<Mentor> Mentors => _mentors;

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

        public bool Equals(StudyStudentGroup? x, StudyStudentGroup? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(StudyStudentGroup obj)
            => obj.Id.GetHashCode();
    }
}