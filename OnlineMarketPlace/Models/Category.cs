using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Category : IEntity<int>
    {
        //public Category()
        //{
        //    Banner = new HashSet<Banner>();
        //    InverseParent = new HashSet<Category>();
        //    ProductAbstract = new HashSet<ProductAbstract>();
        //    SearchFiltersOnCategory = new HashSet<SearchFiltersOnCategory>();
        //    TopSlider = new HashSet<TopSlider>();
        //    UserCategoryVisit = new HashSet<UserCategoryVisit>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string AliasName { get; set; }
        public string TitleAltName { get; set; }
        public int? ParentId { get; set; }
        public string ImageIcon { get; set; }
        public byte[] Image1 { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageForMenu { get; set; }
        public string ImageForMenuPath { get; set; }
        public int? FieldId { get; set; }
        public string Tags { get; set; }
        public string ConnectedLink { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Status { get; set; }
        public int? TotalVisit { get; set; }
        public int? OrderedCount { get; set; }
        public bool ShowInMainPage { get; set; }
        public int? Priority { get; set; }
        public bool ShowInMenu { get; set; }
        public bool ShowInFooter { get; set; }

        [ForeignKey("FieldId")]
        public virtual Field Field { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<ProductAbstract> ProductAbstract { get; set; }
        public virtual ICollection<SearchFiltersOnCategory> SearchFiltersOnCategory { get; set; }
        public virtual ICollection<TopSlider> TopSlider { get; set; }
        public virtual ICollection<UserCategoryVisit> UserCategoryVisit { get; set; }
    }
}
