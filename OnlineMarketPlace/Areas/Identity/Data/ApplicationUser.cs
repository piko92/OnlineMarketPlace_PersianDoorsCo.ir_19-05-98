using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.Models;

namespace OnlineMarketPlace.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public int? Rank { get; set; }
        public DateTime RegisteredDateTime { get; set; }
        public bool SpecialUser { get; set; }
        public bool Status { get; set; }
        public string DefinedByUserId { get; set; }
        public int? Gendre { get; set; }

        [ForeignKey("DefinedByUserId")]
        public virtual ApplicationUser DefinedByUser { get; set; }
        public virtual ICollection<ApplicationUser> InverseDefinedByUser { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<AdminMenu> AdminMenu { get; set; }
        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<ArticleModified> ArticleModified { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<BlockedUser> BlockedUser { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<BrandModified> BrandModified { get; set; }
        public virtual ICollection<Category> Category { get; set; }

        [InverseProperty("ApprovedByUser")]
        public virtual ICollection<Coupon> CouponApprovedByUser { get; set; }
        [InverseProperty("SpecialUser")]
        public virtual ICollection<Coupon> CouponSpecialUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Coupon> CouponUser { get; set; }

        public virtual ICollection<Field> Field { get; set; }
        public virtual ICollection<GeneralPage> GeneralPage { get; set; }
        public virtual ICollection<GeneralPageModified> GeneralPageModified { get; set; }
        public virtual ICollection<GuaranteeProvider> GuaranteeProvider { get; set; }

        [InverseProperty("ApprovedByUser")]
        public virtual ICollection<Invoice> InvoiceApprovedByUser { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Invoice> InvoiceCustomer { get; set; }

        public virtual ICollection<LikeAndDislikeReview> LikeAndDislikeReview { get; set; }
        public virtual ICollection<Manufacturer> Manufacturer { get; set; }
        public virtual ICollection<ManufacturerAddress> ManufacturerAddress { get; set; }
        public virtual ICollection<PhotoGallery> PhotoGallery { get; set; }
        public virtual ICollection<ProductAbstract> ProductAbstract { get; set; }
        public virtual ICollection<ProductDescription> ProductDescription { get; set; }
        public virtual ICollection<ProductFeatureModifed> ProductFeatureModifed { get; set; }
        public virtual ICollection<ProductGuarantee> ProductGuarantee { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductModified> ProductModified { get; set; }
        public virtual ICollection<SearchFilters> SearchFilters { get; set; }
        public virtual ICollection<Seller> Seller { get; set; }
        public virtual ICollection<Setting> Setting { get; set; }
        public virtual ICollection<SiteGeneralInfo> SiteGeneralInfo { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
        public virtual ICollection<TopSlider> TopSlider { get; set; }
        public virtual ICollection<UserCategoryVisit> UserCategoryVisit { get; set; }
        public virtual ICollection<UserFavoriteProduct> UserFavoriteProduct { get; set; }
        public virtual ICollection<UserImage> UserImage { get; set; }
        public virtual ICollection<UserLog> UserLog { get; set; }

        [InverseProperty("ModifedByUser")]
        public virtual ICollection<UserModified> UserModifiedByUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserModified> UserModifiedUser { get; set; }

        [InverseProperty("ApprovedByUser")]
        public virtual ICollection<UserProductReview> UserProductReviewApprovedByUser { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserProductReview> UserProductReviewUser { get; set; }
        public virtual ICollection<UserProductVisit> UserProductVisit { get; set; }

        [InverseProperty("ReportedByUser")]
        public virtual ICollection<UserReported> UserReportedReportedByUser { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserReported> UserReportedUser { get; set; }
        public virtual ICollection<VerificationCode> VerificationCode { get; set; }
    }
}
