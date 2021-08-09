using System.Linq;
using SeaInk.Core.Entities;

namespace SeaInk.Core.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity: IEntity
    {
        IQueryable<TEntity> Values { get; }

        TEntity this[int id] => Values.SingleOrDefault(v => v.Id == id);

        void Create(TEntity value);
        void Update(TEntity value);
        void Delete(TEntity value);
    }
}