using System.Linq;
using Infrastructure.Database;
using SeaInk.Core.Entities;
using SeaInk.Core.Repositories;

namespace Infrastructure.Repositories
{
    public abstract class DbRepositoryBase<T>: IEntityRepository<T> where T: IEntity
    {
        protected DatabaseContext Context;
        public abstract IQueryable<T> Values { get; }

        protected DbRepositoryBase(DatabaseContext context)
        {
            Context = context;
        }

        public void Create(T value)
        {
            Context.Add(value);
            Context.SaveChanges();
        }

        public void Update(T value)
        {
            Context.Update(value);
            Context.SaveChanges();
        }

        public void Delete(T value)
        {
            Context.Remove(value);
            Context.SaveChanges();
        }
    }
}