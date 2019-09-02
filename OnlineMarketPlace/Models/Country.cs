using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Country : IEntity<int>
    {
        //public Country()
        //{
        //    Brand = new HashSet<Brand>();
        //    Province = new HashSet<Province>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public int? ContinentId { get; set; }
        public byte[] Map { get; set; }
        public byte[] Flag { get; set; }
        public string OfficialLanguageFirst { get; set; }
        public string OfficialLanguageSecond { get; set; }
        public float? Population { get; set; }
        public bool Status { get; set; }
        public string Motto { get; set; }
        public byte[] Emblem { get; set; }
        public string Capital { get; set; }
        public string Religion { get; set; }
        public string Gdp { get; set; }
        public string TimeZone { get; set; }
        public int? CallingCode { get; set; }
        public string InternetTld { get; set; }
        public string Coordinates { get; set; }
        public string RangeIp { get; set; }
        public string Comment { get; set; }
        public string Currency { get; set; }
        public string CountryAlphaCode { get; set; }

        [ForeignKey("ContinentId")]
        public virtual Continent Continent { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }
        public virtual ICollection<Province> Province { get; set; }
    }
}
