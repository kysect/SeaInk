using SeaInk.Core.TableLayout.Models;
using SeaInk.Core.Tools;

namespace SeaInk.Core.TableLayout.Exceptions
{
    public class MissingAssignmentComponentException : SeaInkException
    {
        public MissingAssignmentComponentException(AssignmentModel model)
            : base($"There is no component in the table that represents an assignment {model}") { }
    }
}