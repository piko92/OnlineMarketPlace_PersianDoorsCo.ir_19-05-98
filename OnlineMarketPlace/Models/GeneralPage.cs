using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class GeneralPage : IEntity<int>
    {
        //public GeneralPage()
        //{
        //    GeneralPageModified = new HashSet<GeneralPageModified>();
        //    PagesList = new HashSet<PagesList>();
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string AliasTitle { get; set; }
        public string MetaTagJson { get; set; }
        public string MetaDescription { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ContentHtml { get; set; }
        public string StyleCss { get; set; }
        public string ImageIconPath { get; set; }
        public string UserId { get; set; }
        public DateTime RegdDateTime { get; set; }
        public string Tags { get; set; }
        public string Links { get; set; }
        public bool Status { get; set; }
        public byte[] MainImage { get; set; }
        public string MainImagePath { get; set; }

        public int? RelatedProductId { get; set; }

        [ForeignKey("RelatedProductId")]
        public virtual ProductAbstract RelatedProduct { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<GeneralPageModified> GeneralPageModified { get; set; }
        public virtual ICollection<PagesList> PagesList { get; set; }
    }
}
