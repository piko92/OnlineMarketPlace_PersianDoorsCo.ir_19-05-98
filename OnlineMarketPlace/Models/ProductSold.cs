using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ProductSold
    {
        //public ProductSold()
        //{
        //    ProductFeature = new HashSet<ProductFeature>();
        //}

        public int Id { get; set; }
        public int? ProductAbstractId { get; set; }
        public int? ProductFeatureId { get; set; }
        public int? ProductFeatureCode { get; set; }
        public int? Count { get; set; }

        [ForeignKey("ProductAbstractId")]
        public virtual ProductAbstract ProductAbstract { get; set; }

        [ForeignKey("ProductFeatureId")]
        public virtual ProductFeature ProductFeatureNavigation { get; set; }

        [InverseProperty("ProductCodeNavigation")]
        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}
