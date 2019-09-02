using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class BrandModified : IEntity<int>
    {
        public int Id { get; set; }
        public int? BrandId { get; set; }
        public string UserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string Comment { get; set; }
        public string LastBrandBackupJson { get; set; }
        public bool Status { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
