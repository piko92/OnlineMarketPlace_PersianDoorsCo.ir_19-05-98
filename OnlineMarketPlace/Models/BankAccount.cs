using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class BankAccount : IEntity<int>
    {
        //public BankAccount()
        //{
        //    Invoice = new HashSet<Invoice>();
        //    Seller = new HashSet<Seller>();
        //}

        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string CardNo { get; set; }
        public string ShebaNo { get; set; }
        public string Posset { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        public bool OnlineService { get; set; }
        public bool IsPublic { get; set; }
        public bool Status { get; set; }
        public int? BankId { get; set; }
        public DateTime? RegDateTime { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Seller> Seller { get; set; }
    }
}
