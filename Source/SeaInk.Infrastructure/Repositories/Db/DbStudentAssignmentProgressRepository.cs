using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbStudentAssignmentProgressRepository: DbRepositoryBase<StudentAssignmentProgress>
    {
        public override IQueryable<StudentAssignmentProgress> Values => Context.StudentAssignmentProgresses;
        public DbStudentAssignmentProgressRepository(DatabaseContext context): base(context) { }
    }
}