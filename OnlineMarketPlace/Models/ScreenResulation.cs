using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.Models
{
    public partial class ScreenResulation : IEntity<int>
    {
        //public ScreenResulation()
        //{
        //    TopSlider = new HashSet<TopSlider>();
        //}

        public int Id { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Name { get; set; }
        public string LatinName { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Dimensions { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<TopSlider> TopSlider { get; set; }
    }
}
