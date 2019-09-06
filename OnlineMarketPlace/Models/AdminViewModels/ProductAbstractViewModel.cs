using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class ProductAbstractViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("نام  محصول")]
        public string Name { get; set; }

    }
}
