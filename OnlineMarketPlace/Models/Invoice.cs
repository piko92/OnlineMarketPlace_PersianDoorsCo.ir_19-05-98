using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Invoice : IEntity<int>
    {
        //public Invoice()
        //{
        //    InvoiceProduct = new HashSet<InvoiceProduct>();
        //}

        public int Id { get; set; }
        public string Guid { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string CustomerId { get; set; }
        public bool TaxIncluded { get; set; }
        [Column(TypeName = "Money")]
        public decimal? Tax { get; set; }
        public int? ShippingMethodId { get; set; }
        public int? AdditionalPostPriceId { get; set; }
        [Column(TypeName = "Money")]
        public decimal? AdditionalPostPriceAmount { get; set; }
        public string TrackingCode { get; set; }
        public int? SellerId { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public int? CouponId { get; set; }
        public int? CouponIndexId { get; set; }
        public bool Status { get; set; }
        public int? BranchId { get; set; }
        public int? BankId { get; set; }
        public int? BankAccountId { get; set; }
        public bool PaidAtHome { get; set; }
        public bool IsPaid { get; set; }
        public bool OnlinePaid { get; set; }
        public string RecievedTokenFromBank { get; set; }
        public bool Sent { get; set; }
        public bool Delivered { get; set; }
        public bool GiftAvailable { get; set; }
        public bool SendAsGift { get; set; }
        public string GiftAddress { get; set; }
        public string GiftPhoneNumber { get; set; }
        public bool GiftWrap { get; set; }
        public string GiftComment { get; set; }
        [Column(TypeName = "Money")]
        public decimal? CalculatedPrice { get; set; }
        public string Comment { get; set; }
        public DateTime? SendDateTime { get; set; }

        [ForeignKey("AdditionalPostPriceId")]
        public virtual PostPrice AdditionalPostPrice { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public virtual ApplicationUser ApprovedByUser { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccount BankAccount { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }

        [ForeignKey("CouponIndexId")]
        public virtual CouponIndex CouponIndex { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [ForeignKey("ShippingMethodId")]
        public virtual ShippingMethod ShippingMethod { get; set; }

        public virtual ICollection<InvoiceProduct> InvoiceProduct { get; set; }
    }
}
