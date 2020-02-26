using AspCoreBl.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace AspCoreBl.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public PaymentDetailContext _db;
        public DbSet<T> _dbSet;
        public GenericRepository(PaymentDetailContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public T GetByID(int id)
        {
            return _dbSet
                  .Find(id);
        }

        public void Add(T t)
        {
            _dbSet.Add(t);
        }

        public void Update(T t)
        {
            EntityEntry dbEntityEntry = _db.Entry<T>(t);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
        }
        public virtual void SaveChanges()
        {
            _db.SaveChanges();
        }


    }
}
