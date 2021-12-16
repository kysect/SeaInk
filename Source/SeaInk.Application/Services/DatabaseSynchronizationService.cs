using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaInk.Core.Entities;
using SeaInk.Core.Services;
using SeaInk.Core.UniversityServiceModels;
using SeaInk.Infrastructure.DataAccess.Database;

namespace SeaInk.Application.Services;

public class DatabaseSynchronizationService : IDatabaseSynchronizationService
{
    private readonly DatabaseContext _context;
    private readonly IUniversityService _universityService;

    public DatabaseSynchronizationService(DatabaseContext context, IUniversityService universityService)
    {
        _context = context;
        _universityService = universityService;
    }

    public async Task<IReadOnlyCollection<Subject>> SynchronizeSubjectsAsync(
        IReadOnlyCollection<SubjectUniversityModel> models, CancellationToken cancellationToken)
    {
        var subjectModelIds = models.Select(s => s.UniversityId).ToList();

        List<Subject> createdSubjects = await _context.Subjects
            .Where(s => subjectModelIds.Contains(s.UniversityId))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var createdSubjectIds = createdSubjects.Select(s => s.UniversityId).ToList();

        var notCreatedSubjectModels = models
            .Where(m => !createdSubjectIds.Contains(m.UniversityId))
            .ToList();

        var notCreatedSubjects = notCreatedSubjectModels.Select(m => m.ToSubject()).ToList();

        foreach (Subject subject in notCreatedSubjects)
        {
            SubjectDivision? division = await _context.SubjectDivisions
                .SingleOrDefaultAsync(d => d.Subject.Equals(subject), cancellationToken)
                .ConfigureAwait(false);

            if (division is not null)
                continue;

            division = new SubjectDivision(subject);
            _context.SubjectDivisions.Add(division);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return await SynchronizeSubjectsAsync(createdSubjects.Concat(notCreatedSubjects).ToList(), cancellationToken);
    }

    public async Task<IReadOnlyCollection<Subject>> SynchronizeSubjectsAsync(
        IReadOnlyCollection<Subject> subjects, CancellationToken cancellationToken)
    {
        foreach (Subject subject in subjects)
        {
            IReadOnlyCollection<AssignmentUniversityModel> assignmentModels = await _universityService
                .GetSubjectAssignmentsAsync(subject, cancellationToken)
                .ConfigureAwait(false);

            var assignmentModelIds = assignmentModels.Select(a => a.UniversityId).ToList();

            List<Assignment> createdAssignments = await _context.Assignments
                .Where(a => assignmentModelIds.Contains(a.UniversityId))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            var createdAssignmentIds = createdAssignments.Select(a => a.UniversityId).ToList();

            var notCreatedAssignments = assignmentModels
                .Where(m => !createdAssignmentIds.Contains(m.UniversityId))
                .Select(m => m.ToAssignment())
                .ToList();

            Assignment[] assignmentsToAdd = createdAssignments.Concat(notCreatedAssignments).Except(subject.Assignments).ToArray();
            Assignment[] assignmentsToRemove = subject.Assignments.Except(createdAssignments).Except(notCreatedAssignments).ToArray();

            subject.AddAssignments(assignmentsToAdd);
            subject.RemoveAssignments(assignmentsToRemove);

            _context.Subjects.Update(subject);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return subjects;
    }

    public async Task<IReadOnlyCollection<StudentGroup>> SynchronizeStudentGroupsAsync(
        IReadOnlyCollection<StudentGroupUniversityModel> models, CancellationToken cancellationToken)
    {
        var groupModelIds = models.Select(s => s.UniversityId).ToList();

        List<StudentGroup> createdGroups = await _context.StudentGroups
            .Where(s => groupModelIds.Contains(s.UniversityId))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var createdGroupIds = createdGroups.Select(s => s.UniversityId).ToList();

        var notCreatedGroupModels = models
            .Where(m => !createdGroupIds.Contains(m.UniversityId))
            .ToList();

        var notCreatedGroups = notCreatedGroupModels.Select(m => m.ToStudentGroup()).ToList();
        _context.StudentGroups.AddRange(notCreatedGroups);
        await _context.SaveChangesAsync(cancellationToken);

        return await SynchronizeStudentGroupsAsync(createdGroups.Concat(notCreatedGroups).ToList(), cancellationToken);
    }

    public async Task<IReadOnlyCollection<StudentGroup>> SynchronizeStudentGroupsAsync(
        IReadOnlyCollection<StudentGroup> groups, CancellationToken cancellationToken)
    {
        foreach (StudentGroup group in groups)
        {
            IReadOnlyCollection<StudentUniversityModel> studentModels = await _universityService
                .GetGroupStudentsAsync(group, cancellationToken)
                .ConfigureAwait(false);

            var studentModelIds = studentModels.Select(s => s.UniversityId).ToList();

            List<Student> createdStudents = await _context.Students
                .Where(s => studentModelIds.Contains(s.UniversityId))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            var createdStudentIds = createdStudents.Select(s => s.UniversityId).ToList();

            var notCreatedStudents = studentModels
                .Where(s => !createdStudentIds.Contains(s.UniversityId))
                .Select(s => s.ToStudent())
                .ToList();

            Student[] studentsToAdd = createdStudents.Concat(notCreatedStudents).Except(group.Students).ToArray();
            Student[] studentsToRemove = group.Students.Except(createdStudents).Except(notCreatedStudents).ToArray();

            group.AddStudents(studentsToAdd);
            group.RemoveStudents(studentsToRemove);

            _context.StudentGroups.Update(group);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return groups;
    }

    public async Task<IReadOnlyCollection<StudyStudentGroup>> SynchronizeStudyStudentGroupsAsync(
        Subject subject, IReadOnlyCollection<StudentGroup> groups, CancellationToken cancellationToken)
    {
        SubjectDivision division = await _context.SubjectDivisions
            .SingleAsync(d => d.Subject.Equals(subject), cancellationToken)
            .ConfigureAwait(false);

        var createdStudyStudentGroups = division.StudyStudentGroups
            .Where(ssg => groups.Contains(ssg.StudentGroup))
            .ToList();

        StudyStudentGroup[] notCreatedStudyStudentGroups = groups
            .Where(g => !createdStudyStudentGroups.Any(ssg => ssg.StudentGroup.Equals(g)))
            .Select(g => new StudyStudentGroup(g))
            .ToArray();

        division.AddStudentStudyGroups(notCreatedStudyStudentGroups);
        _context.SubjectDivisions.Update(division);
        await _context.SaveChangesAsync(cancellationToken);

        return division.StudyStudentGroups;
    }
}