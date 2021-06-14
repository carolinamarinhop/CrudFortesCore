using CrudFortesCore.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CrudFortesCore.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IContext Context;
        public Repository(IContext context) => Context = context;

        public virtual void Create(T entidade)
        {
            Context.Set<T>().Add(entidade);
            Context.SaveChanges();

        }

        public void Delete(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            Context.SaveChanges();
        }

        public virtual void Edit(T entidade)
        {
            var entityEntry = Context.Entry(entidade);
            entityEntry.State = EntityState.Modified;
            Context.Entry(entidade).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public T Find(int id) => Context.Set<T>().Find(id);

        public IQueryable<T> Query() => Context.Set<T>().AsQueryable();
    }
}
