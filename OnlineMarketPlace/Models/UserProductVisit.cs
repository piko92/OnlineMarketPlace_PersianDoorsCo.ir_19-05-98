using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserProductVisit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public int? ProductId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductAbstract Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
