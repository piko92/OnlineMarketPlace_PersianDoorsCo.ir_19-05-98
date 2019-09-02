using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductImage : IEntity<int>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] BigImage { get; set; }
        public string BigImagePath { get; set; } //2000x2000 size
        public byte[] Image { get; set; }
        public string ImagePath { get; set; } //1000x1000 size
        public byte[] ImageThumbnail { get; set; }
        public string ImageThumbnailPath { get; set; } //275x275 size
        public byte[] ImageTinyThumbnail { get; set; }
        public string ImageTinyThumbnailPath { get; set; } //85x85 size
        public bool Status { get; set; }
        public string VolumeSize { get; set; }
        public string DimensionSize { get; set; }
        public bool GrayScale { get; set; }
        public bool Compressed { get; set; }
        public bool IsMainImage { get; set; }
        public string ImageFormat { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string UserId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductAbstract Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
