using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class TopSlider
    {
        public int Id { get; set; }
        public int? ScreenResulationId { get; set; }
        public int? Priotity { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailImagePath { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? ByteSize { get; set; }
        public string UserId { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string AltName { get; set; }
        public bool HasButton { get; set; }
        public string ButtonContent { get; set; }
        public string ButtonLink { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? ConnectedCategoryId { get; set; }
        public int? ConnectedProductId { get; set; }
        public int? ConnectedBrandId { get; set; }
        public bool SetForFuture { get; set; }
        public DateTime? ShowDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }

        [ForeignKey("ConnectedBrandId")]
        public virtual Brand ConnectedBrand { get; set; }

        [ForeignKey("ConnectedCategoryId")]
        public virtual Category ConnectedCategory { get; set; }

        [ForeignKey("ConnectedProductId")]
        public virtual ProductAbstract ConnectedProduct { get; set; }

        [ForeignKey("ScreenResulationId")]
        public virtual ScreenResulation ScreenResulation { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
