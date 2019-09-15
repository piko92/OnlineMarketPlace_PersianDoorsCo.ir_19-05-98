using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class ContactUs : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime RegdDateTime { get; set; }
        public string Tags { get; set; }
        public bool Status { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByUserId { get; set; }
        [ForeignKey("ApprovedByUserId")]
        public virtual ApplicationUser ApprovedByUser { get; set; }
    }
}
