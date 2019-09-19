using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarket.Models
{
    public partial class ProductAdditionalFeatures : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string Value { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public DateTime? RegDateTime { get; set; }

        public int ProductFeatureId { get; set; }
        [ForeignKey("ProductFeatureId")]
        public virtual ProductFeature ProductFeature { get; set; }

        public int AdditionalFeaturesId { get; set; }
        [ForeignKey("AdditionalFeaturesId")]
        public virtual AdditionalFeatures AdditionalFeatures { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
