using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductGuarantee : IEntity<int>
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? GuaranteeProviderId { get; set; }
        public string GuaranteeType { get; set; }
        [Column(TypeName = "Money")]
        public decimal? ExtraMoney { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Policy { get; set; }
        public string Term { get; set; }
        public string RegDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("GuaranteeProviderId")]
        public virtual GuaranteeProvider GuaranteeProvider { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductAbstract Product { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
