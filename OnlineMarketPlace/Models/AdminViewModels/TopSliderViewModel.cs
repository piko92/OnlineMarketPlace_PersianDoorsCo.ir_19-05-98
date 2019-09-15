using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class TopSliderViewModel
    {
        public int Id { get; set; }
        public int? ScreenResulationId { get; set; }
        public int? Priotity { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        //public string ThumbnailImagePath { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        //public double? Height { get; set; }
        //public double? Width { get; set; }
        //public double? ByteSize { get; set; }
        public string UserId { get; set; }
        //public DateTime? RegDateTime { get; set; }
        public string AltName { get; set; }
        public bool HasButton { get; set; }
        public string ButtonContent { get; set; }
        public string ButtonLink { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        public string Title { get; set; }
        public string Summery { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool SetForFuture { get; set; }
        public DateTime? ShowDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }


        public int? ConnectedCategoryId { get; set; }
        public int? ConnectedProductId { get; set; }
        public int? ConnectedBrandId { get; set; }
        
    }
}
