using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quizzish.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id, bool cache = true);

        IEnumerable<T> GetAll(bool cache = true);
        //IEnumerable<T> Find(Func<T, bool> predicate);
        T Detach(T t);

        void Add(T entity);
        //void AddRange(IEnumerable<T> entities);

        //void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


    }
}
