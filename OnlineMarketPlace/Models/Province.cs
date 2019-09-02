using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Province
    {
        //public Province()
        //{
        //    Address = new HashSet<Address>();
        //    City = new HashSet<City>();
        //    ManufacturerAddress = new HashSet<ManufacturerAddress>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public int? CountryId { get; set; }
        public byte[] Map { get; set; }
        public byte[] Flag { get; set; }
        public float? Population { get; set; }
        public string Coordinates { get; set; }
        public bool Status { get; set; }
        public int? CallingCode { get; set; }
        public string Capital { get; set; }
        public string RangeIp { get; set; }
        public string Language { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<ManufacturerAddress> ManufacturerAddress { get; set; }
    }
}
