using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbUserRepository: DbRepositoryBase<User>
    {
        public override IQueryable<User> Values => Context.Users;

        public DbUserRepository(DatabaseContext context): base(context) { }
    }
}