using System;
using System.Collections.Generic;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudyGroupSubject : IEquatable<StudyGroupSubject>
    {
        private readonly List<Mentor> _mentors = new List<Mentor>();

        public StudyGroupSubject(StudyGroup studyGroup, Subject subject)
        {
            StudyGroup = studyGroup.ThrowIfNull(nameof(studyGroup));
            Subject = subject.ThrowIfNull(nameof(subject));
        }

        public int Id { get; private init; }
        public int SheetId { get; set; }

        // TODO: configure distinct pair of this ids
        public StudyGroup StudyGroup { get; private init; }
        public Subject Subject { get; private init; }
        public IReadOnlyCollection<Mentor> Mentors => _mentors;

        public bool Equals(StudyGroupSubject? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupSubject);

        public override int GetHashCode()
            => Id;

        public void AddMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull(nameof(mentors));
            _mentors.AddRange(mentors);
        }
    }
}