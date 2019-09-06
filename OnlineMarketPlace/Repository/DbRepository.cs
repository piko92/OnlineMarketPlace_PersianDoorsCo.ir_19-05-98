using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository
{
    public class DbRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : OnlineMarketContext
        where TEntity : class, IEntity<TKey>
    {
        TDbContext db;
        public DbRepository(TDbContext _db)
        {
            db = _db;
        }
        public bool DeleteById(TKey Id)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(TKey Id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByName(string Name)
        {
            throw new NotImplementedException();
        }

        public TEntity FindSingleByName(string Name)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public IList<TEntity> Include(Expression<Func<TEntity, object>> Where, params string[] Navigations)
        {
                IQueryable<TEntity> query = null;
                foreach (var include in Navigations)
                {
                    query = db.Set<TEntity>().Include(include);
                }

                return query.ToList();
        }

        public TKey Insert(TEntity Entity)
        {
            db.Add(Entity);
            Save();
            return Entity.Id;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool Update(TEntity Entity)
        {
            throw new NotImplementedException();
        }
    }
}
