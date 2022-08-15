using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Entities;
using SeaInk.Core.StudyTable;
using SeaInk.Core.TableLayout;

namespace SeaInk.Core.Services
{
    public interface ITableDifferenceService
    {
        Task<StudentAssignmentProgressTableDifference> CalculateDifference(
            StudyStudentGroup studyStudentGroup,
            Subject subject,
            TableLayoutComponent layoutComponent,
            CancellationToken cancellationToken);

        Task ApplyDifference(StudyStudentGroup studyStudentGroup, StudentAssignmentProgressTableDifference difference, CancellationToken cancellationToken);
    }
}