using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SeaInk.Core.Entities.Exceptions;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class SubjectDivision : IEqualityComparer<SubjectDivision>
    {
        private readonly List<StudyStudentGroup> _studyStudentGroups = new List<StudyStudentGroup>();

        public SubjectDivision(Subject subject)
        {
            Id = Guid.NewGuid();
            SpreadsheetId = string.Empty;
            Subject = subject.ThrowIfNull();
        }

#pragma warning disable CS8618
        protected SubjectDivision() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public string SpreadsheetId { get; set; }
        public virtual Subject Subject { get; private init; }

        public virtual IReadOnlyCollection<StudyStudentGroup> StudyStudentGroups => _studyStudentGroups;

        public bool Contains(StudyStudentGroup studyStudentGroup)
            => _studyStudentGroups.Contains(studyStudentGroup) && (studyStudentGroup.Division?.Equals(this) ?? false);

        public void AddStudentStudyGroups(params StudyStudentGroup[] studyGroupSubjects)
        {
            studyGroupSubjects.ThrowIfNull();

            if (studyGroupSubjects.Any(Contains))
                throw new ContainingStudyGroupSubjectsException(this);

            foreach (StudyStudentGroup studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.ThrowIfNull();
                studyGroupSubject.Division?.RemoveStudentStudyGroups(studyGroupSubject);
                studyGroupSubject.Division = this;
                _studyStudentGroups.Add(studyGroupSubject);
            }
        }

        public void RemoveStudentStudyGroups(params StudyStudentGroup[] studyGroupSubjects)
        {
            studyGroupSubjects.ThrowIfNull();

            if (studyGroupSubjects.Any(sgs => !Contains(sgs)))
                throw new NotContainingStudyGroupSubjectsException(this);

            foreach (StudyStudentGroup studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.ThrowIfNull();

                _studyStudentGroups.Remove(studyGroupSubject);
                studyGroupSubject.Division = null;
            }
        }

        public bool Equals(SubjectDivision? x, SubjectDivision? y)
            => x is not null && y is not null && x.Id.Equals(y.Id);

        public int GetHashCode(SubjectDivision obj)
            => obj.Id.GetHashCode();
    }
}