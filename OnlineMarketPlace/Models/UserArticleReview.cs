using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarket.Models
{
    public class UserArticleReview : IEntity<int>
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string AnonymousUserEmail { get; set; }
        public string AnonymousUserIp { get; set; }
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
        public bool OpenComment { get; set; }
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("ApprovedByUserId")]
        public virtual ApplicationUser ApprovedByUser { get; set; }

        [ForeignKey("ParentId")]
        public virtual UserArticleReview Parent { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserArticleReview> InverseParent { get; set; }

    }
}
