using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Article : IEntity<int>
    {
        //public Article()
        //{
        //    ArticleModified = new HashSet<ArticleModified>();
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public int? SubjectId { get; set; }
        public string Summery { get; set; }
        public byte[] MainImage { get; set; }
        public string MainImagePath { get; set; }
        public string ContentHtml { get; set; }
        public string StyleCss { get; set; }
        public int? RelatedProductId { get; set; }
        public string UserId { get; set; }
        public DateTime WrittenDateTime { get; set; }
        public bool Status { get; set; }
        public int? CountOfVisit { get; set; }
        public string Writer { get; set; }
        public string Source { get; set; }
        public int? Rank { get; set; }
        public string Tags { get; set; }
        public string Link { get; set; }
        public int? RelatedPhotoGalleryId { get; set; }

        [ForeignKey("RelatedPhotoGalleryId")]
        public virtual PhotoGallery RelatedPhotoGallery { get; set; }

        [ForeignKey("RelatedProductId")]
        public virtual ProductAbstract RelatedProduct { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ArticleModified> ArticleModified { get; set; }
        public virtual ICollection<UserArticleReview> UserArticleReview { get; set; }
    }
}
