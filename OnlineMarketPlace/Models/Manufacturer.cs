using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Manufacturer : IEntity<int>
    {
        //public Manufacturer()
        //{
        //    ManufacturerAddress = new HashSet<ManufacturerAddress>();
        //    ProductAbstract = new HashSet<ProductAbstract>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Field { get; set; }
        public string UserId { get; set; }
        public int? BrandId { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }
        public string Owner { get; set; }
        public byte[] Image { get; set; }
        public DateTime? FoundedDate { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ManufacturerAddress> ManufacturerAddress { get; set; }
        public virtual ICollection<ProductAbstract> ProductAbstract { get; set; }
    }
}
