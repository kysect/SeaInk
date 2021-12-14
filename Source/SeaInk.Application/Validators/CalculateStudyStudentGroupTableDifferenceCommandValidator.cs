using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Commands;
using SeaInk.Core.Entities;
using SeaInk.Core.TableLayout;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Infrastructure.DataAccess.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Validators;

public class CalculateStudyStudentGroupTableDifferenceCommandValidator : AbstractValidator<CalculateStudyStudentGroupTableDifference.Command>
{
    public CalculateStudyStudentGroupTableDifferenceCommandValidator(DatabaseContext context)
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

        RuleFor(c => c.StudyStudentGroupId)
            .MustAsync(async (id, ct) =>
            {
                StudyStudentGroup? ssg = await context.StudyStudentGroups
                    .FindAsync(new object[] { id }, ct)
                    .ConfigureAwait(false);
                ssg = ssg.ThrowIfNull();

                return ssg.Division is not null;
            })
            .WithMessage($"{nameof(StudyStudentGroup)} must be associated with some {nameof(SubjectDivision)}");

        RuleFor(c => c.StudyStudentGroupId)
            .MustAsync(async (id, ct) =>
            {
                StudyGroupSubjectLayout? layout = await context.StudyGroupSubjectLayouts
                    .SingleOrDefaultAsync(l => l.StudyStudentGroup.Id.Equals(id), ct)
                    .ConfigureAwait(false);

                return layout is not null;
            })
            .WithMessage($"{nameof(StudyStudentGroup)} must have {nameof(TableLayoutComponent)} specified");
    }
}