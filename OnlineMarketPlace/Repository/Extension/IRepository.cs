using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository.Extension
{
    interface IRepository<TEntity, TName>
        where TEntity : class, IEntityEx<TName>
    {
        List<TEntity> FindAlikeByName(TName name);
    }
}
