using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Coupon : IEntity<int>
    {
        //public Coupon()
        //{
        //    CouponIndex = new HashSet<CouponIndex>();
        //    Invoice = new HashSet<Invoice>();
        //    Tag = new HashSet<Tag>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool MultiProducts { get; set; }
        public string PolicyJson { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public bool GeneratedCode { get; set; }
        public string CouponCode { get; set; }
        public bool ApplyOnAllGroups { get; set; }
        public bool ForSpecialUsers { get; set; }
        public bool ForSpecialUser { get; set; }
        public string SpecialUserId { get; set; }
        public bool AutoSet { get; set; }
        public bool ApplyOnTag { get; set; }
        public bool AutoIndex { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public virtual ApplicationUser ApprovedByUser { get; set; }
        [ForeignKey("SpecialUserId")]
        public virtual ApplicationUser SpecialUser { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CouponIndex> CouponIndex { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
