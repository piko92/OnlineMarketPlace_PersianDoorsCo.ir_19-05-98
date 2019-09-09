﻿using Microsoft.AspNetCore.Http;
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
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("نام  محصول")]
        public string Name { get; set; }
        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        [DisplayName("برند")]
        public int BrandI { get; set; }
        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        [DisplayName("دسته بندی")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("عکس")]
        public List<IFormFile> img { get; set; }
        [DisplayName("وضعیت")]
        public bool Status { get; set; }
        [DisplayName("قیمت اصلی")]
        public decimal? BasePrice { get; set; }
        public int Code { get; set; }
    }
}
