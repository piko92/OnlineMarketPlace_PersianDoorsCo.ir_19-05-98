using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserProductReview
    {
        //public UserProductReview()
        //{
        //    InverseParent = new HashSet<UserProductReview>();
        //    LikeAndDislikeReview = new HashSet<LikeAndDislikeReview>();
        //}

        public int Id { get; set; }
        public string UserId { get; set; }
        public string AnonymousUserEmail { get; set; }
        public string AnonymousUserIp { get; set; }
        public int? ProductId { get; set; }
        public string Comment { get; set; }
        public int? ParentId { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }
        public bool? Fake { get; set; }
        public string FakeUserName { get; set; }
        public DateTime? FakeDateTime { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public int? Liked { get; set; }
        public int? Disliked { get; set; }
        public bool OpenComment { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public virtual ApplicationUser ApprovedByUser { get; set; }

        [ForeignKey("ParentId")]
        public virtual UserProductReview Parent { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductAbstract Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserProductReview> InverseParent { get; set; }
        public virtual ICollection<LikeAndDislikeReview> LikeAndDislikeReview { get; set; }
    }
}
