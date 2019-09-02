using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserLog
    {
        //public UserLog()
        //{
        //    BlockedUser = new HashSet<BlockedUser>();
        //    SearchedItems = new HashSet<SearchedItems>();
        //    UserBehaviorTracking = new HashSet<UserBehaviorTracking>();
        //}

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public string Action { get; set; }
        public DateTime? LogDate { get; set; }
        public TimeSpan? LogTime { get; set; }
        public bool Status { get; set; }
        public bool MobileOs { get; set; }
        public TimeSpan? SpentTime { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<BlockedUser> BlockedUser { get; set; }
        public virtual ICollection<SearchedItems> SearchedItems { get; set; }
        public virtual ICollection<UserBehaviorTracking> UserBehaviorTracking { get; set; }
    }
}
