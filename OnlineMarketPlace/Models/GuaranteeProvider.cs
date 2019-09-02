using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class GuaranteeProvider : IEntity<int>
    {
        //public GuaranteeProvider()
        //{
        //    ProductGuarantee = new HashSet<ProductGuarantee>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string UserId { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string CatalogPath { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ProductGuarantee> ProductGuarantee { get; set; }
    }
}
