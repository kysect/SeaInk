using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbDivisionRepository: DbRepositoryBase<Division>
    {
        public override IQueryable<Division> Values => Context.Divisions;
        public DbDivisionRepository(DatabaseContext context): base(context) { }
    }
}