using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        public string SenderEmail { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Password { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string ReceiverEmail { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Content { get; set; }

        public List<IFormFile> AttachedFiles { get; set; }
    }
}
