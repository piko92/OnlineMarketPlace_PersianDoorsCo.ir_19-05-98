using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductAbstract : IEntity<int>
    {
        //public ProductAbstract()
        //{
        //    Article = new HashSet<Article>();
        //    Banner = new HashSet<Banner>();
        //    GeneralPage = new HashSet<GeneralPage>();
        //    ProductDescription = new HashSet<ProductDescription>();
        //    ProductFeature = new HashSet<ProductFeature>();
        //    ProductGuarantee = new HashSet<ProductGuarantee>();
        //    ProductImage = new HashSet<ProductImage>();
        //    ProductModified = new HashSet<ProductModified>();
        //    ProductSold = new HashSet<ProductSold>();
        //    ProductTag = new HashSet<ProductTag>();
        //    TopSlider = new HashSet<TopSlider>();
        //    UserFavoriteProduct = new HashSet<UserFavoriteProduct>();
        //    UserProductReview = new HashSet<UserProductReview>();
        //    UserProductVisit = new HashSet<UserProductVisit>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string UserId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime RegDateTime { get; set; }
        public int? ManufacturerId { get; set; }
        public bool Status { get; set; }
        public int? BaseSize { get; set; }
        public double? Weight { get; set; }
        public string Dimensions { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        [Column(TypeName = "Money")]
        public decimal? BasePrice { get; set; }
        public double? Rank { get; set; }
        public bool ContentAvailable { get; set; }
        public int? TolerancQuantity { get; set; }
        public int? TotalVisit { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovedDateTime { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<GeneralPage> GeneralPage { get; set; }
        public virtual ICollection<ProductDescription> ProductDescription { get; set; }
        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
        public virtual ICollection<ProductGuarantee> ProductGuarantee { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductModified> ProductModified { get; set; }
        public virtual ICollection<ProductSold> ProductSold { get; set; }
        public virtual ICollection<ProductTag> ProductTag { get; set; }
        public virtual ICollection<TopSlider> TopSlider { get; set; }
        public virtual ICollection<UserFavoriteProduct> UserFavoriteProduct { get; set; }
        public virtual ICollection<UserProductReview> UserProductReview { get; set; }
        public virtual ICollection<UserProductVisit> UserProductVisit { get; set; }
    }
}
