using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeaInk.Core.Entities;

namespace SeaInk.Application.Services
{
    public interface IMentorService
    {
        Task<Mentor?> FindOrDefaultAsync(Guid mentorId);
        Task<Mentor?> FindOrDefaultAsync(int mentorUniversityId);
        Task<IReadOnlyCollection<StudyGroupSubject>> GetMentorStudyGroupSubjectsAsync(Mentor mentor);
        Task AddMentorStudyGroupSubjectsAsync(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects);
        Task RemoveMentorStudyGroupSubjectsAsync(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects);
    }
}