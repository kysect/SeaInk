using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class MentorService : IMentorService
    {
        private readonly DatabaseContext _context;

        public MentorService(DatabaseContext context)
        {
            _context = context.ThrowIfNull();
        }

        public async Task<Mentor?> FindOrDefaultAsync(Guid mentorId)
        {
            Mentor? mentor = await _context.Mentors.FindAsync(mentorId);
            return mentor;
        }

        public async Task<IReadOnlyCollection<StudyGroupSubject>> GetMentorStudyGroupSubjects(Mentor mentor)
        {
            mentor.ThrowIfNull();

            IQueryable<StudyGroupSubject> studyGroupsSubjects = _context.StudyGroupSubjects
                .Where(s => s.Mentors.Contains(mentor));

            return await studyGroupsSubjects.ToListAsync();
        }

        public Task AddMentorStudyGroupSubjects(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects)
        {
            mentor.ThrowIfNull();
            studyGroupSubjects.ThrowIfNull();

            foreach (StudyGroupSubject studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.AddMentors(mentor);
                _context.StudyGroupSubjects.Update(studyGroupSubject);
            }

            return _context.SaveChangesAsync();
        }

        public Task RemoveMentorStudyGroupSubjects(Mentor mentor, IReadOnlyCollection<StudyGroupSubject> studyGroupSubjects)
        {
            mentor.ThrowIfNull();
            studyGroupSubjects.ThrowIfNull();

            foreach (StudyGroupSubject studyGroupSubject in studyGroupSubjects)
            {
                studyGroupSubject.RemoveMentors(mentor);
                _context.StudyGroupSubjects.Update(studyGroupSubject);
            }

            return _context.SaveChangesAsync();
        }
    }
}