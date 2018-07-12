using System;
using System.Collections.Generic;
using System.Text;

namespace Taleem.BAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
          
        T GetById(int id);

        T GetByGuidId(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
         
    }
}
