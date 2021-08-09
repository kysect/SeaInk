using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbStudyAssignmentRepository: DbRepositoryBase<StudyAssignment>
    {
        public override IQueryable<StudyAssignment> Values => Context.StudyAssignments;
        public DbStudyAssignmentRepository(DatabaseContext context): base(context) { }
    }
}