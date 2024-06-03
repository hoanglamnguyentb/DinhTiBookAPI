using DoAn.Repository.Core;
using DoAn.Repository.OrderDetailRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DoAn.Service.Core
{
    public class Service<T> : IService<T> where T : class
    {
        IRepository<T> _repository;
   

        public Service(IRepository<T> repository) 
        {
            _repository = repository;
        }

        public T? GetById(Guid? guid) 
        { 
            if(guid == null) 
            { 
                return null;
            }
            return _repository.GetById(guid.Value);
        }
        public virtual async Task Create(T entity)
        {
            _repository.Add(entity);
            await _repository.Save();
        }
        public virtual async Task Create(IEnumerable<T> entitis)
        {
            foreach(var entity in entitis) 
            {
                _repository.Add(entity);
            }
            await _repository.Save();
        }
        public virtual async Task Update(T entity)
        {
            _repository.Edit(entity);
            await _repository.Save();
        }
        public virtual async Task Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Edit(entity);
            }
            await _repository.Save();
        }
        public async Task Delete(T entity)
        {
            _repository.Delete(entity);
            await _repository.Save();
        }
        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        
    }
}
