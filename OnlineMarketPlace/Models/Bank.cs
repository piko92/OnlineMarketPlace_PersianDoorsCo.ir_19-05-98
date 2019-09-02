using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Bank : IEntity<int>
    {
        //public Bank()
        //{
        //    BankAccount = new HashSet<BankAccount>();
        //    Invoice = new HashSet<Invoice>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<BankAccount> BankAccount { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
