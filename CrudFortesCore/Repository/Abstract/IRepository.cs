using System.Collections.Generic;
using System.Linq;

namespace CrudFortesCore.Repository.Abstract
{
    public interface IRepository<T> where T : class
    {
        void Create(T entidade);
        void Edit(T entidade);
        void Delete(T entidade);
        IQueryable<T> Query();
        T Find(int id);
    }
}
