using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class PhotoGalleryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Name { get; set; }
        public string LatinName { get; set; }
        [Required(ErrorMessage = " این فیلد الزامیست")]
        public int? SubjectId { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "این فیلد الزامیست")]
        public bool Status { get; set; }
        public string Link { get; set; }
        public string UserId { get; set; }
        public int? RelatedProductId { get; set; }
    }
}
