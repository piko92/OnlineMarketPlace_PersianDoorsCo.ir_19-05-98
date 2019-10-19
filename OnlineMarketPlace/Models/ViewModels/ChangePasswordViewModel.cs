using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DisplayName("کلمه عبور فعلی *")]
        [Required(ErrorMessage = "تکمیل فیلدهای ستاره دار الزامی میباشد")]
        public string CurrentPasword { get; set; }

        [DisplayName("کلمه عبور جدید *")]
        [Required(ErrorMessage = "تکمیل فیلدهای ستاره دار الزامی میباشد")]
        public string Newpassword { get; set; }

        [DisplayName("تکرار کلمه عبور جدید *")]
        [Compare("Newpassword", ErrorMessage = "این فیلد باید تکرار فیلد کلمه عبور جدید باشد")]
        public string NewPasswordConfirm { get; set; }
    }
}
