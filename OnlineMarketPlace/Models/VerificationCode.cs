using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class VerificationCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool? SendToPhoneNumber { get; set; }
        public bool? SendToEmail { get; set; }
        public string GeneratedCode { get; set; }
        public DateTime? RegDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool Status { get; set; }
        public string ReasonOfGenerate { get; set; }
        public bool? AutoGenerate { get; set; }
        public bool? RequestedByAdmin { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
