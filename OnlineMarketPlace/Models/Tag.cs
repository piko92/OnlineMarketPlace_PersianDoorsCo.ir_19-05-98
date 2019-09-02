using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Tag
    {
        //public Tag()
        //{
        //    ProductTag = new HashSet<ProductTag>();
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Deadline { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string UserId { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public int? CouponId { get; set; }
        public int? Priority { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ProductTag> ProductTag { get; set; }
    }
}
