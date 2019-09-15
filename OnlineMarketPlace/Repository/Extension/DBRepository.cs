using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository.Extension
{
    public class DBRepository<TDbContext, TEntity, TName> : IRepository<TEntity, TName>
        where TDbContext : OnlineMarketContext
        where TEntity : class, IEntityEx<TName>
    {
        TDbContext _db;
        public DBRepository(TDbContext db)
        {
            _db = db;
        }
        public List<TEntity> FindAlikeByName(TName name)
        {
            var Result = _db.Set<TEntity>().Where(x => x.Name.ToString().Contains(name.ToString())).ToList();
            return Result;
        }
    }
}
