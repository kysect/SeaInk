using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;

namespace Infrastructure.Repositories.Db
{
    public class DbStudyGroupRepository: DbRepositoryBase<StudyGroup>
    {
        public override IQueryable<StudyGroup> Values => Context.StudyGroups;
        public DbStudyGroupRepository(DatabaseContext context): base(context) { }
    }
}