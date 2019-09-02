using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class PostPrice : IEntity<int>
    {
        //public PostPrice()
        //{
        //    Invoice = new HashSet<Invoice>();
        //}

        public int Id { get; set; }
        public double? Weight { get; set; }
        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }
        public double? Ratio { get; set; }
        public int? WarehouseId { get; set; }
        public long? Distance { get; set; }
        public int? ShippingMethodId { get; set; }
        public bool Extended { get; set; }
        public string Unit { get; set; }
        public DateTime? RegDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ShippingMethodId")]
        public virtual ShippingMethod ShippingMethod { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
