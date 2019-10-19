using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class LoginViewModel
    {
        [MinLength(3)]
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }


        [DisplayName("پسورد")]
        [MinLength(6, ErrorMessage = "حداقل کارکتر ورودی بایستی از 6 عدد، حرف و یا ترکیبی از هردو باشد")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
