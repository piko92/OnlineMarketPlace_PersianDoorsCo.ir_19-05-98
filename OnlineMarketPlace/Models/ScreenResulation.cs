using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class ScreenResulation
    {
        //public ScreenResulation()
        //{
        //    TopSlider = new HashSet<TopSlider>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Dimensions { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<TopSlider> TopSlider { get; set; }
    }
}
