using System;
using SeaInk.Application.TableLayout;
using SeaInk.Core.Entities;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Models
{
    internal sealed class StudyGroupSubjectLayout : IEquatable<StudyGroupSubjectLayout>
    {
        public StudyGroupSubjectLayout(StudyGroupSubject studyGroupSubject, TableLayoutComponent layout)
        {
            StudyGroupSubject = studyGroupSubject.ThrowIfNull();
            Layout = layout.ThrowIfNull();
        }

        public StudyGroupSubject StudyGroupSubject { get; private init; }
        public TableLayoutComponent Layout { get; set; }

        public bool Equals(StudyGroupSubjectLayout? other)
            => other is not null && other.StudyGroupSubject.Id.Equals(StudyGroupSubject.Id);

        public override bool Equals(object? obj)
            => Equals(obj as StudyGroupSubjectLayout);

        public override int GetHashCode()
            => StudyGroupSubject.Id.GetHashCode();
    }
}