using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class SellerDocuments
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string Title { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentPath { get; set; }
        public string Comment { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Status { get; set; }

        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
    }
}
