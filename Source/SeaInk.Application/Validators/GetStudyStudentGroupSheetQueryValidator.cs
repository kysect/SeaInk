using System.Linq;
using FluentValidation;
using SeaInk.Application.Queries;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Validators;

public class GetStudyStudentGroupSheetQueryValidator : AbstractValidator<GetStudyStudentGroupSheet.Query>
{
    public GetStudyStudentGroupSheetQueryValidator(DatabaseContext context)
    {
        RuleFor(q => q.StudyStudentGroupId)
            .MustAsync(async (id, ct) =>
            {
                StudyStudentGroup? ssg = await context.StudyStudentGroups
                    .FindAsync(new object[] { id }, ct)
                    .ConfigureAwait(false);

                return ssg is not null;
            })
            .WithMessage($"{nameof(StudyStudentGroup)} with specified id does not exist");

        RuleFor(q => q.Mentor)
            .MustAsync(async (query, mentor, ct) =>
            {
                StudyStudentGroup? ssg = await context.StudyStudentGroups
                    .FindAsync(new object[] { query.StudyStudentGroupId }, ct)
                    .ConfigureAwait(false);
                ssg = ssg.ThrowIfNull();

                return ssg.Mentors.Contains(mentor);
            })
            .WithMessage($"Given mentor must be associated with this {nameof(StudyStudentGroup)}");
    }
}