using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarket.Models
{
    public partial class Payment : IEntity<int>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string TrackingCode { get; set; }
        public bool? Status { get; set; }
        public bool? Accepted { get; set; }

        [Column(TypeName = "Money")]
        public decimal TotalPaid { get; set; }

        public DateTime RegDateTime { get; set; }
        public DateTime? PaidDateTime { get; set; }
        public string TokenKey { get; set; }

        public int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

    }
}
