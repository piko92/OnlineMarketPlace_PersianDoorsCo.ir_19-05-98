using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد نمایید")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور خود را وارد نمایید")]
        public string Password { get; set; }
        public bool IsRemember { get; set; }

        //[Required(ErrorMessage = "لطفا عبارت درون تصویر را وارد نمایید")]
        //[StringLength(6, ErrorMessage = "تعداد کارکتر وارد شده بیشتر از حد مجاز میباشد")]
        //public string CaptchaCode { get; set; }
    }
}
