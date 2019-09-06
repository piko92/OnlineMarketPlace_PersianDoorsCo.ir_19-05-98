using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository
{
    public interface IRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>
    {
        TEntity FindById(TKey Id);
        TEntity FindSingleByName(string Name);
        List<TEntity> GetAll();
        IList<TEntity> Include(Expression<Func<TEntity, object>> Where, params string[] Navigations);
        List<TEntity> FindByName(string Name);
        TKey Insert(TEntity Entity);
        bool Update(TEntity Entity);
        bool DeleteById(TKey Id);
        void Save();
    }
}
