using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Core.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class StudyGroupSubject : IEquatable<StudyGroupSubject>
    {
        private readonly List<Mentor> _mentors = new List<Mentor>();

        public StudyGroupSubject(StudyGroup studyGroup, Subject subject)
        {
            Id = Guid.NewGuid();
            StudyGroup = studyGroup.ThrowIfNull();
            Subject = subject.ThrowIfNull();
        }

        public Guid Id { get; private init; }
        public int? SheetId { get; set; }

        public Division? Division { get; internal set; }

        // TODO: configure distinct pair of this ids
        public StudyGroup StudyGroup { get; private init; }
        public Subject Subject { get; private init; }
        public IReadOnlyCollection<Mentor> Mentors => _mentors;

        public bool Equals(StudyGroupSubject? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupSubject);

        public override int GetHashCode()
            => Id.GetHashCode();

        public void AddMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull();
            _mentors.AddRange(mentors);
        }

        public void RemoveMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull();

            var errors = mentors
                .Select((m, i) => (I: i, Result: _mentors.Remove(m)))
                .Where(p => !p.Result)
                .Select(p => new RemovingMentorException(this, mentors[p.I]))
                .ToList();

            if (errors.Any())
            {
                throw new AggregateException(errors);
            }
        }
    }
}