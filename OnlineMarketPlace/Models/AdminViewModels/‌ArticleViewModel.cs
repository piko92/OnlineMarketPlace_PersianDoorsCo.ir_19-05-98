using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "پر کردن این فیلد الزامیست")]
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("عنوان لاتین")]
        public string LatinTitle { get; set; }
        public int? SubjectId { get; set; }
        [DisplayName(" خلاصه مقاله")]
        public string Summery { get; set; }
        [DisplayName(" تصویر اصلی مقاله")]
        public IFormFile MainImage { get; set; }
        public string MainImagePath { get; set; }
        [DisplayName(" متن اصلی")]
        public string ContentHtml { get; set; }
        public string StyleCss { get; set; }
        [DisplayName(" کالای مرتبط")]
        public int? RelatedProductId { get; set; }
        public string UserId { get; set; }
        [DisplayName(" وضعیت")]
        public bool Status { get; set; }
        public int? CountOfVisit { get; set; }
        public string Writer { get; set; }
        public string Source { get; set; }
        public int? Rank { get; set; }
        public string Tags { get; set; }
        public string Link { get; set; }
        public int? RelatedPhotoGalleryId { get; set; }

    }
}
