using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class PhotoGallery : IEntity<int>
    {
        //public PhotoGallery()
        //{
        //    Article = new HashSet<Article>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public int? SubjectId { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public DateTime RegDateTime { get; set; }
        public string Link { get; set; }
        public string UserId { get; set; }
        public int? RelatedProductId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Article> Article { get; set; }
    }
}
