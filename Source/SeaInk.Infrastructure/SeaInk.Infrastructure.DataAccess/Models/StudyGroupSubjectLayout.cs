using System;
using System.ComponentModel.DataAnnotations;
using SeaInk.Core.Entities;
using SeaInk.Core.TableLayout;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.DataAccess.Models
{
    public sealed class StudyGroupSubjectLayout : IEquatable<StudyGroupSubjectLayout>
    {
        public StudyGroupSubjectLayout(StudyStudentGroup studyStudentGroup, TableLayoutComponent layout)
        {
            StudyStudentGroup = studyStudentGroup.ThrowIfNull();
            Layout = layout.ThrowIfNull();
        }

#pragma warning disable CS8618
        private StudyGroupSubjectLayout() { }
#pragma warning restore CS8618

        [Key]
        public Guid Id { get; private init; }

        public StudyStudentGroup StudyStudentGroup { get; private init; }
        public TableLayoutComponent Layout { get; set; }

        public bool Equals(StudyGroupSubjectLayout? other)
            => other is not null && other.StudyStudentGroup.Id.Equals(StudyStudentGroup.Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupSubjectLayout);

        public override int GetHashCode()
            => StudyStudentGroup.Id.GetHashCode();
    }
}