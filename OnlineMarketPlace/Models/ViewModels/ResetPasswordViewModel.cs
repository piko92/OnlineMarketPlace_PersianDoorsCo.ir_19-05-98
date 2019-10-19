using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DisplayName("کد تایید")]
        [Required(ErrorMessage = "تکمیل فیلدهای ستاره دار الزامی میباشد")]
        public string Token { get; set; }

        [Required(ErrorMessage = "تکمیل فیلدهای ستاره دار الزامی میباشد")]
        public string Key { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "تکمیل فیلدهای ستاره دار الزامی میباشد")]
        public string Password { get; set; }

        [DisplayName("تکرار کلمه عبور")]
        [Compare("Password",ErrorMessage = "این فیلد باید تکرار کلمه عبور باشد")]
        public string ConfirmPassword { get; set; }

    }
}
