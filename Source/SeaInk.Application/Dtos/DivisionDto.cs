using System;
using System.Collections.Generic;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public sealed class DivisionDto : IEquatable<DivisionDto>
    {
        public DivisionDto(Guid id, string title, string spreadsheetId, IReadOnlyCollection<StudyGroupSubjectDto> studyGroupSubjects)
        {
            Id = id;
            Title = title.ThrowIfNull();
            SpreadsheetId = spreadsheetId.ThrowIfNull();
            StudyGroupSubjects = studyGroupSubjects.ThrowIfNull();
        }

        public Guid Id { get; }
        public string Title { get; }
        public string SpreadsheetId { get; }
        public IReadOnlyCollection<StudyGroupSubjectDto> StudyGroupSubjects { get; }

        public bool Equals(DivisionDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as DivisionDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}