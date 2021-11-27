using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Models;

namespace SeaInk.Application.TableLayout.Visitors
{
    public interface ITableRowVisitor
    {
        StudentModel GetStudent();
        AssignmentProgress? GetProgress(AssignmentModel assignment);

        void SetStudent(StudentModel student);
        void AddProgress(AssignmentModel assignment, AssignmentProgress progress);
    }
}