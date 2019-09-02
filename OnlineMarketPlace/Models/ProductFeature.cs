using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductFeature : IEntity<int>
    {
        //public ProductFeature()
        //{
        //    CouponIndex = new HashSet<CouponIndex>();
        //    InvoiceProduct = new HashSet<InvoiceProduct>();
        //    ProductFeatureModifed = new HashSet<ProductFeatureModifed>();
        //    ProductPrice = new HashSet<ProductPrice>();
        //    ProductSold = new HashSet<ProductSold>();
        //}

        public int Id { get; set; }
        public int ProductAbstractId { get; set; }
        public int? ProductCode { get; set; }
        public string UserId { get; set; }
        public int? SellerId { get; set; }
        public long? ProductFeatureCode { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int? TextureId { get; set; }
        public int? MaterialId { get; set; }
        public int? TotalCount { get; set; }
        public int? Count { get; set; }
        public bool Active { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Status { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public int? WarehouseId { get; set; }
        public bool Original { get; set; }
        public bool ForSale { get; set; }
        public int? MinimumForSale { get; set; }
        public int? MaximumForSale { get; set; }
        public int? MinForWholeSale { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [ForeignKey("ProductAbstractId")]
        public virtual ProductAbstract ProductAbstract { get; set; }

        [ForeignKey("ProductCodeNavigationId")]
        public virtual ProductSold ProductCodeNavigation { get; set; }

        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [ForeignKey("SizeId")]
        public virtual Sizes Size { get; set; }

        [ForeignKey("TextureId")]
        public virtual Texture Texture { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        public virtual ICollection<CouponIndex> CouponIndex { get; set; }
        public virtual ICollection<InvoiceProduct> InvoiceProduct { get; set; }
        public virtual ICollection<ProductFeatureModifed> ProductFeatureModifed { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }

        [InverseProperty("ProductFeatureNavigation")]
        public virtual ICollection<ProductSold> ProductSold { get; set; }
    }
}
