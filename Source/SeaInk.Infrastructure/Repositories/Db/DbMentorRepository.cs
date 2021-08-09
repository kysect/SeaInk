using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbMentorRepository: DbRepositoryBase<Mentor>
    {
        public override IQueryable<Mentor> Values => Context.Mentors;
        public DbMentorRepository(DatabaseContext context): base(context) { }
    }
}