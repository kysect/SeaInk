using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeaInk.Application.Dtos;
using SeaInk.Core.Entities;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Queries
{
    public static class GetMentorSubjects
    {
        public record Query(Mentor Mentor) : IRequest<IReadOnlyCollection<SubjectDto>>;

        public class Handler : IRequestHandler<Query, IReadOnlyCollection<SubjectDto>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context.ThrowIfNull();
            }

            public async Task<IReadOnlyCollection<SubjectDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Subject> subjects = await _context.SubjectDivisions
                    .Where(d => d.StudyStudentGroups.Any(g => g.Mentors.Contains(request.Mentor)))
                    .Select(d => d.Subject)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                return subjects.Select(s => s.ToDto()).ToList();
            }
        }
    }
}