using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserCategoryVisit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? VisitedDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
