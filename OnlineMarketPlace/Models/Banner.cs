using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Banner : IEntity<int>
    {
        public int Id { get; set; }
        public int? ScreenResulotionId { get; set; }
        public string Position { get; set; }
        public int? ShowInPageId { get; set; }
        public int? TypeId { get; set; }
        public int? Priority { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string Format { get; set; }
        public bool Active { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool SetForFuture { get; set; }
        public DateTime? ShowDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public bool Status { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? ByteSize { get; set; }
        public string AltName { get; set; }
        public bool HasButton { get; set; }
        public string ButtonContent { get; set; }
        public string ButtonLink { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string Description { get; set; }
        public int? ConnectedCategoryId { get; set; }
        public int? ConnectedProductId { get; set; }
        public int? ConnectedBrandId { get; set; }
        public string UserId { get; set; }
        public string CollectionName { get; set; }
        public string Link { get; set; }
        public bool Animated { get; set; }
        public bool? GrayScaleOn { get; set; }

        [ForeignKey("ConnectedBrandId")]
        public virtual Brand ConnectedBrand { get; set; }

        [ForeignKey("ConnectedCategoryId")]
        public virtual Category ConnectedCategory { get; set; }

        [ForeignKey("ConnectedProductId")]
        public virtual ProductAbstract ConnectedProduct { get; set; }

        [ForeignKey("ShowInPageId")]
        public virtual PagesList ShowInPage { get; set; }

        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
