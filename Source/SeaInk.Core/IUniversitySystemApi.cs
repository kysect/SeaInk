using System.Collections.Generic;
using SeaInk.Core.Models;

namespace SeaInk.Core
{
    public interface IUniversitySystemApi
    {
        List<string> GetGroupsByMentor(int mentorId);
        List<string> GetGroupsBySubject(string subjectName);
        List<string> GetStudentsByGroup(string groupName);

        void SendPoints(List<SubjectActivity> results);
    }
}