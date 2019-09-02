using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ShippingMethod
    {
        //public ShippingMethod()
        //{
        //    Invoice = new HashSet<Invoice>();
        //    PostPrice = new HashSet<PostPrice>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool? AllowForGuest { get; set; }
        public bool? AllowChangeCountry { get; set; }
        public bool? AllowChangeProvince { get; set; }
        public bool? AllowChangeCity { get; set; }
        public bool Status { get; set; }
        public string Policy { get; set; }

        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "Money")]
        public decimal? DynamicPrice { get; set; }
        public bool OnlyPartner { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PostPrice> PostPrice { get; set; }
    }
}
