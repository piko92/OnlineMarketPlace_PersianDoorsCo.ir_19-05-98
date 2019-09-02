using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class CouponIndex : IEntity<int>
    {
        //public CouponIndex()
        //{
        //    Invoice = new HashSet<Invoice>();
        //}

        public int Id { get; set; }
        public int? CouponId { get; set; }
        public bool AutoClear { get; set; }
        public int? ProductId { get; set; }
        public bool Status { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public double? DiscountAmount { get; set; }
        [Column(TypeName = "Money")]
        public decimal? OldPrice { get; set; }
        [Column(TypeName = "Money")]
        public decimal? NewPrice { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductFeature Product { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
