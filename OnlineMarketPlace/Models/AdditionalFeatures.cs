using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class AdditionalFeatures : IEntity<int>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public int? ParentId { get; set; }
        public string ImageIcon { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ParentId")]
        public virtual AdditionalFeatures Parent { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<AdditionalFeatures> InverseParent { get; set; }
        public virtual ICollection<ProductAdditionalFeatures> ProductAdditionalFeatures { get; set; }
        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}
