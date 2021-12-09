using System.Threading.Tasks;
using SeaInk.Application.TableLayout;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface ILayoutService
    {
        Task SaveLayoutAsync(StudyGroupSubject studyGroupSubject, TableLayoutComponent layout);
        Task<TableLayoutComponent?> GetLayoutAsync(StudyGroupSubject studyGroupSubject);
    }
}