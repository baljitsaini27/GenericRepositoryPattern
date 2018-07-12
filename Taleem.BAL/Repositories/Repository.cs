using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taleem.BAL.Interfaces;
using Taleem.Data.TaleemEntities;

namespace Taleem.BAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TaleemDbContext _context;

        public Repository(TaleemDbContext context)
        {
            _context = context;
        }
        protected void Save() => _context.SaveChanges();

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        } 
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        } 
        public T GetByGuidId(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Insert(T entity)
        {
            _context.Add(entity);
            Save();
        } 
        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }   
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
