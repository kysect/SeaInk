using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Core.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Division : IEquatable<Division>
    {
        private readonly List<StudyGroupSubject> _studyGroupSubjects = new List<StudyGroupSubject>();

        public Division(string title)
        {
            Id = Guid.NewGuid();
            Title = title.ThrowIfNull();
            SpreadsheetId = string.Empty;
        }

        [Key]
        public Guid Id { get; private init; }

        public string Title { get; private init; }
        public string SpreadsheetId { get; set; }

        public IReadOnlyCollection<StudyGroupSubject> StudyGroupSubjects => _studyGroupSubjects;

        public bool Equals(Division? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Division);

        public override int GetHashCode()
            => Id.GetHashCode();

        public void AddStudyGroupSubjects(params StudyGroupSubject[] studyGroupSubjects)
        {
            studyGroupSubjects.ThrowIfNull();

            foreach (StudyGroupSubject studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.ThrowIfNull();
                if (studyGroupSubject.Division?.Equals(this) ?? false)
                    continue;

                studyGroupSubject.Division?.RemoveStudyGroupSubjects(studyGroupSubject);
                studyGroupSubject.Division = this;
                _studyGroupSubjects.Add(studyGroupSubject);
            }
        }

        public void RemoveStudyGroupSubjects(params StudyGroupSubject[] studyGroupSubjects)
        {
            studyGroupSubjects.ThrowIfNull();

            foreach (StudyGroupSubject studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.ThrowIfNull();
                if (studyGroupSubject.Division is null || !studyGroupSubject.Division.Equals(this))
                    throw new ForeignStudyGroupSubjectException(this, studyGroupSubject);

                if (!_studyGroupSubjects.Remove(studyGroupSubject))
                    throw new InvalidOperationException($"Trying to remove {nameof(StudyGroupSubject)} that not present");
            }
        }
    }
}