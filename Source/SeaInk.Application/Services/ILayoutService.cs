using SeaInk.Application.TableLayout;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface ILayoutService
    {
        void SaveLayout(StudyGroupSubject studyGroupSubject, TableLayoutComponent layout);
        TableLayoutComponent GetLayout(StudyGroupSubject studyGroupSubject);
    }
}