using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class Mentor : IEqualityComparer<Mentor>
    {
        private readonly List<StudyStudentGroup> _studyStudentGroups = new List<StudyStudentGroup>();

        public Mentor(int universityId, string firstName, string lastName, string middleName)
        {
            Id = Guid.NewGuid();
            UniversityId = universityId;
            FirstName = firstName.ThrowIfNull();
            MiddleName = middleName.ThrowIfNull();
            LastName = lastName.ThrowIfNull();
        }

#pragma warning disable CS8618
        protected Mentor() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public int UniversityId { get; private init; }

        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string MiddleName { get; private init; }

        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public virtual IReadOnlyCollection<StudyStudentGroup> StudyStudentGroups => _studyStudentGroups.AsReadOnly();

        public bool Equals(Mentor? x, Mentor? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(Mentor obj)
            => obj.Id.GetHashCode();
    }
}