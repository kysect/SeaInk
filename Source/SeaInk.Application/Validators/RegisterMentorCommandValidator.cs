using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Commands;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;

namespace SeaInk.Application.Validators;

public class RegisterMentorCommandValidator : AbstractValidator<RegisterMentor.Command>
{
    public RegisterMentorCommandValidator(DatabaseContext context)
    {
        RuleFor(c => c.MentorUniversityId)
            .MustAsync(async (id, ct) =>
            {
                Mentor? mentor = await context.Mentors
                    .SingleOrDefaultAsync(m => m.UniversityId == id, ct)
                    .ConfigureAwait(false);

                return mentor is null;
            })
            .WithMessage("Mentor with given id is already registered");
    }
}