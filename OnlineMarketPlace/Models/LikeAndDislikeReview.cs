using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class LikeAndDislikeReview : IEntity<int>
    {
        public int Id { get; set; }
        public int? UserProductReviewId { get; set; }
        public string UserId { get; set; }
        public int? Reaction { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("UserProductReviewId")]
        public virtual UserProductReview UserProductReview { get; set; }
    }
}
