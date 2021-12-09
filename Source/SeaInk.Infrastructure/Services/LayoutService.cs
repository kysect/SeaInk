using System.Threading.Tasks;
using SeaInk.Application.Services;
using SeaInk.Application.TableLayout;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.Database;
using SeaInk.Infrastructure.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly DatabaseContext _context;

        public LayoutService(DatabaseContext context)
        {
            _context = context.ThrowIfNull();
        }

        public async Task SaveLayoutAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layout)
        {
            StudyGroupSubjectLayout? studyGroupSubjectLayout = await _context.StudyGroupSubjectLayouts.FindAsync(studyGroupSubject.Id);

            if (studyGroupSubjectLayout is null)
            {
                _context.StudyGroupSubjectLayouts.Add(new StudyGroupSubjectLayout(studyGroupSubject, layout));
            }
            else
            {
                studyGroupSubjectLayout.Layout = layout;
                _context.Update(studyGroupSubjectLayout);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<TableLayoutComponent?> GetLayoutAsync(StudyGroupSubject studyGroupSubject)
        {
            StudyGroupSubjectLayout? studyGroupSubjectLayout = await _context.StudyGroupSubjectLayouts.FindAsync(studyGroupSubject.Id);
            return studyGroupSubjectLayout?.Layout;
        }
    }
}