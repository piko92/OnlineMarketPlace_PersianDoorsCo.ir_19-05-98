using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Repository.Extension
{
    public interface IEntityEx<TName>
    {
        TName Name { get; set; }
    }
}