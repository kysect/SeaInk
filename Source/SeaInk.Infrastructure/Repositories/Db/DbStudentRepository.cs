using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbStudentRepository: DbRepositoryBase<Student>
    {
        public override IQueryable<Student> Values => Context.Students;

        public DbStudentRepository(DatabaseContext context): base(context) { }
    }
}