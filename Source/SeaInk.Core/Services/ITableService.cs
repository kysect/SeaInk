using System.Threading;
using System.Threading.Tasks;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Core.TableLayout;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.Services
{
    public interface ITableService
    {
        Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(
            StudyStudentGroup studyStudentGroup, TableLayoutComponent layoutComponent, CancellationToken cancellationToken);

        Task<CreateSheetResponse> CreateSheetAsync(
            StudyStudentGroup studyStudentGroup, TableLayoutComponent layoutComponent, CancellationToken cancellationToken);

        Task<SheetLink> GetSheetLinkAsync(StudyStudentGroup studyStudentGroup, CancellationToken cancellationToken);

        Task WriteDataToSheetAsync(
            SubjectDivision subjectDivision,
            StudyStudentGroup studyStudentGroup,
            TableLayoutComponent layoutComponent,
            TableModel tableModel,
            CancellationToken cancellationToken);

        Task<TableModel> GetDataFromSheetAsync(
            SubjectDivision subjectDivision,
            StudyStudentGroup studyStudentGroup,
            TableLayoutComponent layoutComponent,
            CancellationToken cancellationToken);
    }
}