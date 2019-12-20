using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public int? ScreenResulotionId { get; set; }
        public string Position { get; set; }
        public int? ShowInPageId { get; set; }
        public int? TypeId { get; set; }
        public int? Priority { get; set; }
        //
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string Format { get; set; }
        public bool Active { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool SetForFuture { get; set; }
        public DateTime? ShowDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public bool Status { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? ByteSize { get; set; }
        public string AltName { get; set; }
        public bool HasButton { get; set; }
        //ButtonContent
        public string ButtonContent { get; set; }
        //ButtonLink
        public string ButtonLink { get; set; }
        //Title
        public string Title { get; set; }
        public string Summery { get; set; }
        public string Description { get; set; }
        public int? ConnectedCategoryId { get; set; }
        public int? ConnectedProductId { get; set; }
        public int? ConnectedBrandId { get; set; }
        public string UserId { get; set; }
        public string CollectionName { get; set; }
        //Link
        public string Link { get; set; }
        public bool Animated { get; set; }
        public bool? GrayScaleOn { get; set; }


    }
}
