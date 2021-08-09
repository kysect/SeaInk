using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbSubjectRepository: DbRepositoryBase<Subject>
    {
        public override IQueryable<Subject> Values => Context.Subjects;
        public DbSubjectRepository(DatabaseContext context): base(context) { }
    }
}