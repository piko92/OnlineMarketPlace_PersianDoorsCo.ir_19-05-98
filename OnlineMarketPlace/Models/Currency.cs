using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Currency : IEntity<int>
    {
        //public Currency()
        //{
        //    ProductPrice = new HashSet<ProductPrice>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Symbol { get; set; }
        public bool Status { get; set; }

        [Column(TypeName = "Money")]
        public decimal? RatioPrice { get; set; }

        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    }
}
