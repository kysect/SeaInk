using System.Collections.Generic;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class StudyGroupSubject
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

        internal void AddMentors(params Mentor[] mentors)
        {
            mentors.ThrowIfNull(nameof(mentors));
            _mentors.AddRange(mentors);
        }
    }
}