using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public sealed class Division : IEquatable<Division>
    {
        private readonly List<StudyGroupSubject> _studyGroupSubjects = new List<StudyGroupSubject>();

        public Division(string title)
        {
            Title = title.ThrowIfNull(nameof(title));
            SpreadsheetId = string.Empty;
        }

        [Key]
        public int Id { get; private init; }

        public string Title { get; private init; }
        public string SpreadsheetId { get; private init; }

        public IReadOnlyCollection<StudyGroupSubject> StudyGroupSubjects => _studyGroupSubjects;

        public bool Equals(Division? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as Division);

        public override int GetHashCode()
            => Id;

        public void AddStudyGroupSubjects(params StudyGroupSubject[] studyGroupSubjects)
        {
            studyGroupSubjects.ThrowIfNull(nameof(studyGroupSubjects));
            _studyGroupSubjects.AddRange(studyGroupSubjects);
        }
    }
}