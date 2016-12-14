using CallCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CallCenter.Data.Repository
{
    public interface IRepositoryBase<T> where T : class, IEntityBase, new()
    {
        IEnumerable<T> GetAll();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        int Count();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        Task<bool> Commit();
    }
}
