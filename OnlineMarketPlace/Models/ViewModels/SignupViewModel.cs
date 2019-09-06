using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "ورود نام الزامیست")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "ورود نام خانوادگی الزامیست")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "با پسورد وارد شده همخوانی ندارد")]
        public string ConfirmPassword { get; set; }

        public IFormFile img { get; set; }
        public string Phonenumber { get; set; }
        public string Nationalcode { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public bool? Gender { get; set; }
        public bool Specialuser { get; set; }
        public bool Status { get; set; }
        public string DefinedByUserId { get; set; }
    }
}
