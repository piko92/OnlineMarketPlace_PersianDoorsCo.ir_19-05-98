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
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("ایمیل")]
        public string UserName { get; set; }
        [DisplayName("پسورد")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
