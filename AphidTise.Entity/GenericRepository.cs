using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidTise.Entity
{
    public class GenericRepository<T> : IGenericRepository<T>
   where T : class
    {
        public jobseeders_com_aphidbyteEntities context = new jobseeders_com_aphidbyteEntities();
        public virtual T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            T entity = context.Set<T>().Where(predicate).FirstOrDefault();
            return entity;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = context.Set<T>();
            return query;
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual int Save()
        {
            return context.SaveChanges();
        }


        public virtual void Edit(T entity)
        {
            context.Entry(entity).State = System.Data.EntityState.Modified;
                  
        }

        public virtual T FirstElement()
        {
            return context.Set<T>().FirstOrDefault();
        }


    }
}
