using DoAn.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DoAnContext _context;
        private DbSet<T> _dbSet;

        public Repository(DoAnContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbSet.Where(predicate).AsEnumerable();
            return query;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable<T>();
        }

        public T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public virtual async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
