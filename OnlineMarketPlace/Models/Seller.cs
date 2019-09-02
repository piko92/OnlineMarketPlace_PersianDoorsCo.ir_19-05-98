using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Seller
    {
        //public Seller()
        //{
        //    Branch = new HashSet<Branch>();
        //    Invoice = new HashSet<Invoice>();
        //    ProductFeature = new HashSet<ProductFeature>();
        //    SellerDocuments = new HashSet<SellerDocuments>();
        //}

        public int Id { get; set; }
        public string UserId { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? BankAccountId { get; set; }
        public bool Status { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccount BankAccount { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
        public virtual ICollection<SellerDocuments> SellerDocuments { get; set; }
    }
}
