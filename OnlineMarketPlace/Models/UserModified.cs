using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class UserModified: IEntity<int>
    {
        public int Id { get; set; }
        public string ModifedByUserId { get; set; }
        public string UserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string LastUserBackupJson { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }


        [ForeignKey("ModifedByUserId")]
        public virtual ApplicationUser ModifedByUser { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
