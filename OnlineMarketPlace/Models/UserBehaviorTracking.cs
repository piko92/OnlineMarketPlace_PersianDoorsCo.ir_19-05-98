using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserBehaviorTracking
    {
        public int Id { get; set; }
        public int? UserLogId { get; set; }
        public string MouseOrTouchBehavior { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserLogId")]
        public virtual UserLog UserLog { get; set; }
    }
}
