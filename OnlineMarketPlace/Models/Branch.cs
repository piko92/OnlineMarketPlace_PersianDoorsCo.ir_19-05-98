using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Branch : IEntity<int>
    {
        //public Branch()
        //{
        //    Invoice = new HashSet<Invoice>();
        //    Warehouse = new HashSet<Warehouse>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public string UserId { get; set; }
        public int? SellerId { get; set; }
        public string Address { get; set; }
        public byte[] Logo { get; set; }
        public string LogoPath { get; set; }
        public string SubDomain { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public bool Status { get; set; }
        public DateTime? RegDateTime { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
