using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductFeatureModifed : IEntity<int>
    {
        public int Id { get; set; }
        public int? ProductFeatureId { get; set; }
        public string ModifedByUserId { get; set; }
        public string LastRecordBackupJson { get; set; }
        public DateTime? ModifedDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ModifedByUserId")]
        public virtual ApplicationUser ModifedByUser { get; set; }

        [ForeignKey("ProductFeatureId")]
        public virtual ProductFeature ProductFeature { get; set; }
    }
}
