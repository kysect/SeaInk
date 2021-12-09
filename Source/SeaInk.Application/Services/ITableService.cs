using System.Threading.Tasks;
using SeaInk.Application.Models;
using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface ITableService
    {
        Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent);
        Task<CreateSheetResponse> CreateSheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent);

        Task WriteDataToSheetAsync(Division division, StudyGroupSubject studyGroupSubject, TableModel tableModel);
        Task<TableModel> GetDataFromSheetAsync(Division division, StudyGroupSubject studyGroupSubject);
    }
}