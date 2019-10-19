using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class VerifyViewModel
    {
        [DisplayName("کد تایید")]
        [Required(ErrorMessage = "تکمیل این فیلد الزامی میباشد")]
        public string Token { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "تکمیل این فیلد الزامی میباشد")]
        public string UserName { get; set; }
    }
}
