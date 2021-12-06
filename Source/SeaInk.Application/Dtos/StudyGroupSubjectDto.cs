using System;
using System.Collections.Generic;
using System.Linq;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Dtos
{
    public sealed class StudyGroupSubjectDto : IEquatable<StudyGroupSubjectDto>
    {
        public StudyGroupSubjectDto(Guid id, int sheetId, StudyGroupDto studyGroup, SubjectDto subjectDto, IReadOnlyCollection<MentorDto> mentors)
        {
            Id = id;
            SheetId = sheetId;
            StudyGroup = studyGroup.ThrowIfNull();
            SubjectDto = subjectDto.ThrowIfNull();
            Mentors = mentors.ThrowIfNull().ToList();
        }

        public Guid Id { get; }
        public int SheetId { get; }
        public StudyGroupDto StudyGroup { get; }
        public SubjectDto SubjectDto { get; }
        public IReadOnlyCollection<MentorDto> Mentors { get; }

        public bool Equals(StudyGroupSubjectDto? other)
            => other is not null && other.Id.Equals(Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupSubjectDto);

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}