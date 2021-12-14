using System;
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

namespace SeaInk.Application.Queries;

public static class GetMentorSubjectStudyStudentGroups
{
    public record Query(Mentor Mentor, Guid SubjectId) : IRequest<IReadOnlyCollection<StudyStudentGroupDto>>;

    public class Handler : IRequestHandler<Query, IReadOnlyCollection<StudyStudentGroupDto>>
    {
        private readonly DatabaseContext _context;

        public Handler(DatabaseContext context)
        {
            _context = context.ThrowIfNull();
        }

        public async Task<IReadOnlyCollection<StudyStudentGroupDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            SubjectDivision division = await _context.SubjectDivisions
                .SingleAsync(d => d.Subject.Id.Equals(request.SubjectId), cancellationToken)
                .ConfigureAwait(false);

            var studyStudentGroups = division.StudyStudentGroups
                .Where(ssg => ssg.Mentors.Contains(request.Mentor))
                .ToList();

            return studyStudentGroups.Select(s => s.ToDto()).ToList();
        }
    }
}