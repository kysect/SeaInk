using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Models;
using SeaInk.Application.Services;
using SeaInk.Core.Entities;
using SeaInk.Core.Models;
using SeaInk.Infrastructure.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Infrastructure.Services
{
    public class UniversityServiceDatabaseDecorator : IUniversityService
    {
        private readonly IUniversityService _universityService;
        private readonly DatabaseContext _context;

        public UniversityServiceDatabaseDecorator(IUniversityService universityServiceImplementation, DatabaseContext context)
        {
            _universityService = universityServiceImplementation.ThrowIfNull();
            _context = context.ThrowIfNull();
        }

        public async Task<Mentor> GetMentorAsync(int universityId)
        {
            Mentor? mentor = await _context.Mentors.SingleOrDefaultAsync(m => m.UniversityId == universityId);

            if (mentor is null)
            {
                mentor = await _universityService.GetMentorAsync(universityId);
                _context.Mentors.Add(mentor);
                await _context.SaveChangesAsync();
            }

            return mentor;
        }

        public Task<IReadOnlyCollection<SubjectModel>> GetMentorSubjectsAsync(Mentor mentor)
            => _universityService.GetMentorSubjectsAsync(mentor);

        public Task<IReadOnlyCollection<StudyGroupModel>> GetMentorSubjectGroupsAsync(Mentor mentor, Subject subject)
            => _universityService.GetMentorSubjectGroupsAsync(mentor, subject);

        public async Task<Subject> GetSubjectAsync(SubjectModel subjectModel)
        {
            Subject? subject = await _context.Subjects.SingleOrDefaultAsync(s => s.UniversityId == subjectModel.UniversityId);

            subject = await (subject is null ? _universityService.GetSubjectAsync(subjectModel) : _universityService.UpdateSubjectAsync(subject));
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<Subject> UpdateSubjectAsync(Subject subject)
        {
            subject = await _universityService.UpdateSubjectAsync(subject);
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<StudyGroup> GetGroupAsync(StudyGroupModel groupModel)
        {
            StudyGroup? group = await _context.StudyGroups.SingleOrDefaultAsync(s => s.UniversityId == groupModel.UniversityId);

            group = await (group is null ? _universityService.GetGroupAsync(groupModel) : _universityService.UpdateGroupAsync(group));
            _context.StudyGroups.Update(group);
            await _context.SaveChangesAsync();

            return group;
        }

        public async Task<StudyGroup> UpdateGroupAsync(StudyGroup group)
        {
            group = await _universityService.UpdateGroupAsync(group);
            _context.StudyGroups.Update(group);
            await _context.SaveChangesAsync();

            return group;
        }

        public Task<StudentsAssignmentProgressTable> GetStudentAssignmentProgressTableAsync(StudyGroupSubject studyGroupSubject)
            => _universityService.GetStudentAssignmentProgressTableAsync(studyGroupSubject);

        public Task SetStudentAssignmentProgressesAsync(IReadOnlyCollection<StudentAssignmentProgress> progresses)
            => _universityService.SetStudentAssignmentProgressesAsync(progresses);
    }
}