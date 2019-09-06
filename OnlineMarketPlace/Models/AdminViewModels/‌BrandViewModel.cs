using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("نام برند ")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

    }
}
