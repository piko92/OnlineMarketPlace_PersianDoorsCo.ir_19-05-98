using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.ClassLibraries.DataSeed;

namespace OnlineMarketPlace.Areas.Identity.Data
{
    public class OnlineMarketContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineMarketContext(DbContextOptions<OnlineMarketContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<AdditionalFeatures> AdditionalFeatures { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AdminMenu> AdminMenu { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleModified> ArticleModified { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<BlockedUser> BlockedUser { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<BrandModified> BrandModified { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Continent> Continent { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<CouponIndex> CouponIndex { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<GeneralPage> GeneralPage { get; set; }
        public virtual DbSet<GeneralPageModified> GeneralPageModified { get; set; }
        public virtual DbSet<GuaranteeProvider> GuaranteeProvider { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceProduct> InvoiceProduct { get; set; }
        public virtual DbSet<LikeAndDislikeReview> LikeAndDislikeReview { get; set; }
        public virtual DbSet<LoginFailure> LoginFailure { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<ManufacturerAddress> ManufacturerAddress { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<PagesList> PagesList { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PhotoGallery> PhotoGallery { get; set; }
        public virtual DbSet<PostPrice> PostPrice { get; set; }
        public virtual DbSet<ProductAbstract> ProductAbstract { get; set; }
        public virtual DbSet<ProductAdditionalFeatures> ProductAdditionalFeatures { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductFeature> ProductFeature { get; set; }
        public virtual DbSet<ProductFeatureModifed> ProductFeatureModifed { get; set; }
        public virtual DbSet<ProductGuarantee> ProductGuarantee { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductModified> ProductModified { get; set; }
        public virtual DbSet<ProductPrice> ProductPrice { get; set; }
        public virtual DbSet<ProductSold> ProductSold { get; set; }
        public virtual DbSet<ProductTag> ProductTag { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<ScreenResulation> ScreenResulation { get; set; }
        public virtual DbSet<SearchFilters> SearchFilters { get; set; }
        public virtual DbSet<SearchFiltersOnCategory> SearchFiltersOnCategory { get; set; }
        public virtual DbSet<SearchedItems> SearchedItems { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
        public virtual DbSet<SellerDocuments> SellerDocuments { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethod { get; set; }
        public virtual DbSet<SiteGeneralInfo> SiteGeneralInfo { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Texture> Texture { get; set; }
        public virtual DbSet<TopSlider> TopSlider { get; set; }
        public virtual DbSet<OnlineMarket.Models.Type> Types { get; set; }
        public virtual DbSet<UserArticleReview> UserArticleReview { get; set; }
        public virtual DbSet<UserBehaviorTracking> UserBehaviorTracking { get; set; }
        public virtual DbSet<UserCategoryVisit> UserCategoryVisit { get; set; }
        public virtual DbSet<UserFavoriteProduct> UserFavoriteProduct { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<UserModified> UserModified { get; set; }
        public virtual DbSet<UserProductReview> UserProductReview { get; set; }
        public virtual DbSet<UserProductVisit> UserProductVisit { get; set; }
        public virtual DbSet<UserReported> UserReported { get; set; }
        public virtual DbSet<VerificationCode> VerificationCode { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(b => b.RegisteredDateTime).HasDefaultValueSql("getdate()");
                entity.Property(b => b.Status).HasDefaultValue(true);
                entity.Property(b => b.SpecialUser).HasDefaultValue(false);
            });
            builder.Entity<Address>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<AdminMenu>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Article>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.WrittenDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ArticleModified>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Bank>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<BankAccount>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.IsPublic).HasDefaultValue(false);
                entity.Property(p => p.Active).HasDefaultValue(true);
                entity.Property(p => p.OnlineService).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Banner>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Active).HasDefaultValue(true);
                entity.Property(p => p.SetForFuture).HasDefaultValue(false);
                entity.Property(p => p.HasButton).HasDefaultValue(false);
                entity.Property(p => p.Animated).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<BlockedUser>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Branch>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<BlockedUser>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Brand>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<BrandModified>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Category>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ShowInMainPage).HasDefaultValue(true);
                entity.Property(p => p.ShowInFooter).HasDefaultValue(false);
                entity.Property(p => p.ShowInMenu).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<City>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Color>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Continent>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Country>(entity =>
            {
                entity.Property(p => p.Id).ValueGeneratedNever();
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Coupon>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.MultiProducts).HasDefaultValue(true);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.GeneratedCode).HasDefaultValue(false);
                entity.Property(p => p.ApplyOnAllGroups).HasDefaultValue(false);
                entity.Property(p => p.ForSpecialUsers).HasDefaultValue(false);
                entity.Property(p => p.ForSpecialUser).HasDefaultValue(true);
                entity.Property(p => p.AutoSet).HasDefaultValue(false);
                entity.Property(p => p.ApplyOnTag).HasDefaultValue(false);
                entity.Property(p => p.AutoIndex).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<CouponIndex>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.AutoClear).HasDefaultValue(false);
            });
            builder.Entity<Currency>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Field>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<GeneralPage>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegdDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<GeneralPageModified>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<GuaranteeProvider>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Invoice>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.TaxIncluded).HasDefaultValue(false);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.PaidAtHome).HasDefaultValue(false);
                entity.Property(p => p.OnlinePaid).HasDefaultValue(false);
                entity.Property(p => p.IsPaid).HasDefaultValue(false);
                entity.Property(p => p.Sent).HasDefaultValue(false);
                entity.Property(p => p.Delivered).HasDefaultValue(false);
                entity.Property(p => p.GiftAvailable).HasDefaultValue(false);
                entity.Property(p => p.SendAsGift).HasDefaultValue(false);
                entity.Property(p => p.GiftWrap).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<InvoiceProduct>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<LikeAndDislikeReview>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<LoginFailure>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ManufacturerAddress>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Material>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<PagesList>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Payment>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Accepted).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<PhotoGallery>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<PostPrice>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductAbstract>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ContentAvailable).HasDefaultValue(false);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductAdditionalFeatures>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductDescription>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductFeature>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.Original).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductFeatureModifed>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductGuarantee>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductImage>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.GrayScale).HasDefaultValue(false);
                entity.Property(p => p.Compressed).HasDefaultValue(false);
                entity.Property(p => p.IsMainImage).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductModified>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ProductPrice>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.AutoSync).HasDefaultValue(false);
                entity.Property(p => p.BasedOnForeignCurrency).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Province>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<ScreenResulation>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<SearchedItems>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<SearchFilters>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<SearchFiltersOnCategory>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Seller>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.Pending).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<SellerDocuments>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Setting>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.IsStatusActive).HasDefaultValue(true);
                entity.Property(p => p.IsMarketPlace).HasDefaultValue(false);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<ShippingMethod>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<SiteGeneralInfo>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.SetForMainPage).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Sizes>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<Subject>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Tag>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Deadline).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Texture>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<TopSlider>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.HasButton).HasDefaultValue(false);
                entity.Property(p => p.Active).HasDefaultValue(true);
                entity.Property(p => p.SetForFuture).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            //builder.Entity<Types>(entity =>
            //{
            //    entity.Property(p => p.Status).HasDefaultValue(true);
            //});
            builder.Entity<UserBehaviorTracking>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });
            builder.Entity<UserCategoryVisit>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.VisitedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserFavoriteProduct>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserImage>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserLog>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.LogDate).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserModified>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.ModifiedDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserProductReview>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.Fake).HasDefaultValue(false);
                entity.Property(p => p.OpenComment).HasDefaultValue(false);
                entity.Property(p => p.Approved).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserProductVisit>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.VisitDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<UserReported>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<VerificationCode>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
                entity.Property(p => p.AutoGenerate).HasDefaultValue(false);
                entity.Property(p => p.RequestedByAdmin).HasDefaultValue(false);
                entity.Property(p => p.RegDateTime).HasDefaultValueSql("getdate()");
            });
            builder.Entity<Warehouse>(entity =>
            {
                entity.Property(p => p.Status).HasDefaultValue(true);
            });

            builder.Seed();
        }
    }
}
