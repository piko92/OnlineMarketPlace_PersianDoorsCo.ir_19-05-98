using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductDescription : IEntity<int>
    {
        public int Id { get; set; }
        public string SummaryHtml { get; set; }
        public string LatinSummaryHtml { get; set; }
        public string DescriptionHtml { get; set; }
        public string LatinDescriptionHtml { get; set; }
        public string DescriptionStyle { get; set; }
        public string FeatureHtml { get; set; }
        public string FeatureStyle { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public DateTime RegDateTime { get; set; }
        public int ProductAbstractId { get; set; }
        [ForeignKey("ProductAbstractId")]
        public virtual ProductAbstract ProductAbstract { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
