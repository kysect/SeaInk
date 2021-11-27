using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
{
    public class AssignmentProgressModel
    {
        public AssignmentProgressModel(AssignmentModel assignment, AssignmentProgress progress)
        {
            Assignment = assignment.ThrowIfNull(nameof(assignment));
            Progress = progress.ThrowIfNull(nameof(progress));
        }

        public AssignmentModel Assignment { get; }
        public AssignmentProgress Progress { get; }
    }
}