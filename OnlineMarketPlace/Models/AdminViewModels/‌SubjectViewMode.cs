using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "انتخاب این فیلد الزامیست")]
        public bool Status { get; set; }
    }
}
