using System;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
{
    public sealed class AssignmentModel : IEquatable<AssignmentModel>
    {
        public AssignmentModel(string title)
        {
            Title = title.ThrowIfNull(nameof(title));
        }

        public string Title { get; private init; }
        public bool? IsMilestone { get; private init; }

        public DateTime? Deadline { get; private init; }

        public double? MinPoints { get; private init; }
        public double? MaxPoints { get; private init; }

        public bool Equals(AssignmentModel? other)
            => other is not null && other.Title.Equals(Title);

        public override bool Equals(object? obj)
            => Equals(obj as AssignmentModel);

        public override int GetHashCode()
            => Title.GetHashCode();

        public override string ToString()
            => Title;
    }
}