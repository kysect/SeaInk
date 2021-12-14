using SeaInk.Core.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Models
{
    public class AssignmentProgressModel
    {
        public AssignmentProgressModel(AssignmentModel assignment, AssignmentProgress progress)
        {
            Assignment = assignment.ThrowIfNull();
            Progress = progress.ThrowIfNull();
        }

        public AssignmentModel Assignment { get; }
        public AssignmentProgress Progress { get; }
    }
}