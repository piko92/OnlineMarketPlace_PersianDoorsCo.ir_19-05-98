using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("نام  محصول")]
        public string Name { get; set; }

        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        [DisplayName("برند")]
        public int BrandI { get; set; }

        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        [DisplayName("دسته بندی")]
        public int CategoryId { get; set; }

        [DisplayName("تصاویر")]
        public List<IFormFile> img { get; set; }

        // [Required]
        [DisplayName("تصویر‌اصلی")]
        public IFormFile MainImage { get; set; } //تصویر اصلی

        [DisplayName("وضعیت")]
        public bool Status { get; set; }

        [DisplayName("قیمت اصلی")]
        public decimal? BasePrice { get; set; }

        public int Code { get; set; }
    }
}
