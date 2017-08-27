using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AphidTise.Entity
{

    public interface IGenericRepository<T>
   {
       T Get(Expression<Func<T, bool>> predicate);
       IQueryable<T> GetAll();
       IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
       void Add(T entity);
       void Edit(T entity);
       void Delete(T entity);
       int Save();
   }
}
