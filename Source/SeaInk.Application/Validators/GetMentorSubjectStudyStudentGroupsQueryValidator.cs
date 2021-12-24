using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Queries;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;

namespace SeaInk.Application.Validators;

public class GetMentorSubjectStudyStudentGroupsQueryValidator : AbstractValidator<GetMentorSubjectStudyStudentGroups.Query>
{
    public GetMentorSubjectStudyStudentGroupsQueryValidator(DatabaseContext context)
    {
        RuleFor(q => q.SubjectId)
            .MustAsync(async (id, ct) =>
            {
                Subject? subject = await context.Subjects
                    .FindAsync(new object[] { id }, ct)
                    .ConfigureAwait(false);

                return subject is not null;
            })
            .WithMessage($"{nameof(Subject)} with specified id does not exist");

        RuleFor(q => q.Mentor)
            .MustAsync(async (query, mentor, ct) =>
            {
                SubjectDivision division = await context.SubjectDivisions
                    .SingleAsync(d => d.Subject.Id.Equals(query.SubjectId), ct)
                    .ConfigureAwait(false);

                return division.StudyStudentGroups.Any(ssg => ssg.Mentors.Contains(mentor));
            })
            .WithMessage($"{nameof(Mentor)} must be associated with specified subject");
    }
}