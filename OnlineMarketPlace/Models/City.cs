using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class City : IEntity<int>
    {
        //public City()
        //{
        //    Address = new HashSet<Address>();
        //    Branch = new HashSet<Branch>();
        //    ManufacturerAddress = new HashSet<ManufacturerAddress>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public int? ProvinceId { get; set; }
        public byte[] Map { get; set; }
        public byte[] Flag { get; set; }
        public byte[] Image { get; set; }
        public bool Status { get; set; }
        public string RangeIp { get; set; }
        public string Coordinates { get; set; }
        public int? CallingCode { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<ManufacturerAddress> ManufacturerAddress { get; set; }
    }
}
