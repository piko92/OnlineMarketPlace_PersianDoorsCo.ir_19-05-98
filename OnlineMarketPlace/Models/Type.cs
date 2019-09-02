using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class Type
    {
        //public Type()
        //{
        //    Banner = new HashSet<Banner>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Banner> Banner { get; set; }
    }
}
