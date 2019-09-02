using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Address: IEntity<int>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserAddress { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostalCode { get; set; }
        public bool Status { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public DateTime? RegDateTime { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
       
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
