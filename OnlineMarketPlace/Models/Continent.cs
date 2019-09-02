using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Continent : IEntity<int>
    {
        //public Continent()
        //{
        //    Country = new HashSet<Country>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string RangeIp { get; set; }
        public string Coordinates { get; set; }
        public byte[] Map { get; set; }
        public byte[] Flag { get; set; }
        public string Color { get; set; }
        public bool Status { get; set; }
        public long? Population { get; set; }

        public virtual ICollection<Country> Country { get; set; }
    }
}
