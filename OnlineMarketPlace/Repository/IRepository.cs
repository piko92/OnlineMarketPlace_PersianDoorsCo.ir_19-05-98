using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository
{
    public interface IRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey>
    {
        TEntity FindById(TKey Id);
        TEntity FindSingleByName(string Name);
        List<TEntity> GetAll();
        List<TEntity> FindByName(string Name);
        TKey Insert(TEntity Entity);
        bool Update(TEntity Entity);
        bool DeleteById(TKey Id);
        void Save();
    }
}
