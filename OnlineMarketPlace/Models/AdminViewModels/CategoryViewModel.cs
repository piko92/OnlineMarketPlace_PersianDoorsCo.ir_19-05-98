using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("نام دسته بندی")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        public string LatinName { get; set; }
        public string AliasName { get; set; }
        public string TitleAltName { get; set; }
    }
}
