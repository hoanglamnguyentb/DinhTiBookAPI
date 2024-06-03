using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.Core
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll ();
        IEnumerable<T> FindBy (Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        Task Save();
        T? GetById(object id);
        void DeleteRange(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);
       

    }
}
