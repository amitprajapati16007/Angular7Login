using AspCoreBl.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

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
            EntityEntry dbEntityEntry = _dbSet.Attach(t);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
        }
        public virtual void Save()
        {
            _db.SaveChanges();
        }


    }
}
