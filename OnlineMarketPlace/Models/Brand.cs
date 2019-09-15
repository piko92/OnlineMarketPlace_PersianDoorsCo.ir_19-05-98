using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineMarketPlace.Repository.Extension;

namespace OnlineMarket.Models
{
    public partial class Brand : IEntity<int>, IEntityEx<string>
    {
        //public Brand()
        //{
        //    Banner = new HashSet<Banner>();
        //    BrandModified = new HashSet<BrandModified>();
        //    Manufacturer = new HashSet<Manufacturer>();
        //    ProductAbstract = new HashSet<ProductAbstract>();
        //    TopSlider = new HashSet<TopSlider>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string UserId { get; set; }
        public int? CountryId { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Catalog { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? RegDateTime { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<BrandModified> BrandModified { get; set; }
        public virtual ICollection<Manufacturer> Manufacturer { get; set; }
        public virtual ICollection<ProductAbstract> ProductAbstract { get; set; }
        public virtual ICollection<TopSlider> TopSlider { get; set; }
    }
}
