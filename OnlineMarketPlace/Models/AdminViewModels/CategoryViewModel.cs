using Microsoft.AspNetCore.Http;
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
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("نام دسته بندی")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("اسم لاتین")]
        public string LatinName { get; set; }
        public string AliasName { get; set; }
        public string TitleAltName { get; set; }
        [DisplayName("زیر مجموعه دسته بندی")]
        public int ParentId { get; set; }
        [DisplayName("عکس")]
        public IFormFile Image1 { get; set; }
        public IFormFile ImageForMenu { get; set; }
        public string ConnectedLink { get; set; }
        [DisplayName("وضعیت")]
        public bool Status { get; set; }
        public bool ShowInMainPage { get; set; }
        public bool ShowInMenu { get; set; }
        public bool ShowInFooter { get; set; }
        public int? Priority { get; set; }
    }
}
