using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class BlockedUser : IEntity<int>
    {
        public int Id { get; set; }
        public int? UserLogId { get; set; }
        public bool? Block { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool? AutoBlock { get; set; }
        public string ByUserId { get; set; }
        public string Reason { get; set; }
        public int? LoginFailureId { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ByUserId")]
        public virtual ApplicationUser ByUser { get; set; }

        [ForeignKey("LoginFailureId")]
        public virtual LoginFailure LoginFailure { get; set; }

        [ForeignKey("UserLogId")]
        public virtual UserLog UserLog { get; set; }
    }
}
