using System.Threading.Tasks;
using SeaInk.Application.Models;
using SeaInk.Application.Services;
using SeaInk.Application.TableLayout;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class TableServiceDatabaseDecorator : ITableService
    {
        private readonly DatabaseContext _context;
        private readonly ITableService _service;

        public TableServiceDatabaseDecorator(DatabaseContext context, ITableService service)
        {
            _context = context.ThrowIfNull();
            _service = service.ThrowIfNull();
        }

        public async Task<CreateSpreadsheetResponse> CreateSpreadsheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent)
        {
            CreateSpreadsheetResponse response = await _service.CreateSpreadsheetAsync(studyGroupSubject, layoutComponent);

            Division division = studyGroupSubject.Division.ThrowIfNull();

            _context.Divisions.Update(division);
            _context.StudyGroupSubjects.Update(studyGroupSubject);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<CreateSheetResponse> CreateSheetAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layoutComponent)
        {
            CreateSheetResponse response = await _service.CreateSheetAsync(studyGroupSubject, layoutComponent);

            Division division = studyGroupSubject.Division.ThrowIfNull();

            _context.Divisions.Update(division);
            _context.StudyGroupSubjects.Update(studyGroupSubject);
            await _context.SaveChangesAsync();

            return response;
        }

        public Task WriteDataToSheetAsync(Division division, StudyGroupSubject studyGroupSubject, TableModel tableModel)
            => _service.WriteDataToSheetAsync(division, studyGroupSubject, tableModel);

        public Task<TableModel> GetDataFromSheetAsync(Division division, StudyGroupSubject studyGroupSubject)
            => _service.GetDataFromSheetAsync(division, studyGroupSubject);
    }
}