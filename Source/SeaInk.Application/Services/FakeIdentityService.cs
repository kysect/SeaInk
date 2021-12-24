using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Commands;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;

namespace SeaInk.Application.Services;

public class FakeIdentityService : IIdentityService
{
    private readonly DatabaseContext _context;
    private readonly IMediator _mediator;

    public FakeIdentityService(DatabaseContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Mentor> GetCurrentMentor()
    {
        Mentor? mentor = await _context.Mentors.FirstOrDefaultAsync().ConfigureAwait(false);

        if (mentor is null)
        {
            var command = new RegisterMentor.Command(10);
            mentor = await _mediator.Send(command).ConfigureAwait(false);
        }

        return mentor;
    }
}