using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Color : IEntity<int>
    {
        //public Color()
        //{
        //    ProductFeature = new HashSet<ProductFeature>();
        //}

        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorLatinName { get; set; }
        public string HexCode { get; set; }
        public string Rgba { get; set; }
        public bool Status { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}
