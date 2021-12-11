using System.Threading.Tasks;
using SeaInk.Application.Models;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface ITableDifferenceService
    {
        Task<StudentAssignmentProgressTableDifference> CalculateDifference(StudyGroupSubject studyGroupSubject);
        Task ApplyDifference(StudyGroupSubject studyGroupSubject, StudentAssignmentProgressTableDifference difference);
    }
}