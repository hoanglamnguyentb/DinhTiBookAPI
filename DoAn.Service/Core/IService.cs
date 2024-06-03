using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Core
{
    public interface IService<T> where T : class
    {
        T? GetById(Guid? id);
        Task Create(T entity);
        Task Create(IEnumerable<T> entities);
        Task Update(T entity);
        Task Update(IEnumerable<T> entities);
        Task Delete(T entity);
        IQueryable<T> GetQueryable();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
