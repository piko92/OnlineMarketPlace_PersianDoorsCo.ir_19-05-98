using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام و نام خانوادگی گیرنده")]
        [Required(ErrorMessage ="تکمیل این فیلد الزامی میباشد")]
        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayName("تلفن تماس")]
        [Required(ErrorMessage ="تکمیل این فیلد الزامی میباشد")]
        public string MobilePhoneNumber { get; set; }

        [DisplayName("آدرس")]
        [Required(ErrorMessage ="تکمیل این فیلد الزامی میباشد")]
        public string Address { get; set; }

        [DisplayName("کد پستی")]
        public string PostalCode { get; set; }
    }
}
