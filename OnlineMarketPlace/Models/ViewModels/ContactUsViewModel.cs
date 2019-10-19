using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Name { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
       // [EmailAddress(ErrorMessage = "آدرس ایمیل را صحیح وارد نمایید")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "آدرس ایمیل را صحیح وارد نمایید")]

        public string Email { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Comment { get; set; }
    }
}
