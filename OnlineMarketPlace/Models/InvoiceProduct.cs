using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class InvoiceProduct : IEntity<int>
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProductFeatureId { get; set; }
        public int? Count { get; set; }
        [Column(TypeName = "Money")]
        public decimal? RawPrice { get; set; }
        public bool Status { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        [ForeignKey("ProductFeatureId")]
        public virtual ProductFeature ProductFeature { get; set; }
    }
}
