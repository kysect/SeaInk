using System.Linq;
using FluentValidation;
using SeaInk.Application.Commands;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Validators;

public class ApplyStudyStudentGroupTableDifferenceCommandValidator : AbstractValidator<ApplyStudyStudentGroupTableDifference.Command>
{
    public ApplyStudyStudentGroupTableDifferenceCommandValidator(DatabaseContext context)
    {
        RuleFor(c => c.StudyStudentGroupId)
            .MustAsync(async (id, ct) =>
            {
                StudyStudentGroup? ssg = await context.StudyStudentGroups
                    .FindAsync(new object[] { id }, ct)
                    .ConfigureAwait(false);

                return ssg is not null;
            })
            .WithMessage($"{nameof(StudyStudentGroup)} with specified id does not exist");

        RuleFor(c => c.Mentor)
            .MustAsync(async (command, mentor, ct) =>
            {
                StudyStudentGroup? ssg = await context.StudyStudentGroups
                    .FindAsync(new object[] { command.StudyStudentGroupId }, ct)
                    .ConfigureAwait(false);
                ssg = ssg.ThrowIfNull();

                return ssg.Mentors.Contains(mentor);
            })
            .WithMessage($"Mentor must be associated with specified {nameof(StudyStudentGroup)}");
    }
}