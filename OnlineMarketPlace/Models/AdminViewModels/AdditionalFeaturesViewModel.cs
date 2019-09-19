using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class AdditionalFeaturesViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("نام دسته بندی")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("اسم لاتین")]
        public string LatinName { get; set; }
        public int? ParentId { get; set; }
        [DisplayName("عکس")]
        public IFormFile Image { get; set; }
        [DisplayName("وضعیت")]
        public bool Status { get; set; }
        public string UserId { get; set; }
    }
}
