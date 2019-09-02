using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Material : IEntity<int>
    {
        //public Material()
        //{
        //    ProductFeature = new HashSet<ProductFeature>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public byte[] Icon { get; set; }
        public string IconPath { get; set; }
        public bool Status { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}
