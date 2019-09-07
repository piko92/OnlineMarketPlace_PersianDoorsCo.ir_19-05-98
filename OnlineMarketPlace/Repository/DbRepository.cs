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
        //DeleteById
        public bool DeleteById(TKey Id)
        {
            var entity = this.db.Set<TEntity>().Find(Id);
            if (entity != null)
            {
                db.Set<TEntity>().Remove(entity);
                Save();
                return true;
            }
            else
            {
                return false;
            }

        }
        //FindById
        public TEntity FindById(TKey Id)
        {
            return db.Set<TEntity>().Find(Id);
        }
        //FindByName
        public List<TEntity> FindByName(string Name)
        {
            throw new NotImplementedException();
        }
        //FindSingleByName
        public TEntity FindSingleByName(string Name)
        {
            throw new NotImplementedException();
        }
        //GetAll
        public List<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }
        //GetInclude
        public IList<TEntity> GetInclude(params Expression<Func<TEntity, Object>>[] includes)
        {
            IQueryable<TEntity> query = null;

            if (includes.Length > 0)
            {
                query = db.Set<TEntity>().Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query.ToList();
        }
        //Insert
        public TKey Insert(TEntity Entity)
        {
            db.Add(Entity);
            Save();
            return Entity.Id;
        }
        //Update
        public bool Update(TEntity Entity)
        {
            var entity = this.db.Set<TEntity>().Find(Entity.Id);
            if (entity != null)
            {
                entity = Entity;
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        //Save
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
