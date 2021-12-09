using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface IMentorService
    {
        Task<Mentor?> FindOrDefaultAsync(Guid mentorId);
        Task<IReadOnlyCollection<StudyGroupSubject>> GetMentorStudyGroupSubjects(Mentor mentor);
        Task AddMentorStudyGroupSubjects(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects);
        Task RemoveMentorStudyGroupSubjects(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects);
    }
}