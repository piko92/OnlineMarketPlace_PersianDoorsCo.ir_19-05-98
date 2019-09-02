using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserReported
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Report { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }
        public string ReportedByUserId { get; set; }
        public string Determination { get; set; }

        [ForeignKey("ReportedByUserId")]
        public virtual ApplicationUser ReportedByUser { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
