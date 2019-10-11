using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
       // [RegularExpression(@"/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/", ErrorMessage = "کلمه عبور وارد شده باید شامل اعداد و حروف و حداقل 6 کارکتر باشد.")]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [Compare("Password", ErrorMessage = "این فیلد بایستی با رمز عبور یکسان باشد.")]
        [DisplayName("تکرار رمز عبور")]
        public string PasswordConfirm { get; set; }
        public bool AcceptPolicy { get; set; }
    }
}
