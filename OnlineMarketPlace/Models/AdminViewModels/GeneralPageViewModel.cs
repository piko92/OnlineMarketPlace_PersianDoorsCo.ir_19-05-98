using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class GeneralPageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string AliasTitle { get; set; }
        public string MetaTagJson { get; set; }
        public string MetaDescription { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ContentHtml { get; set; }
        public string StyleCss { get; set; }
        public string ImageIconPath { get; set; }
        public string UserId { get; set; }
        public DateTime RegdDateTime { get; set; }
        public string Tags { get; set; }
        public string Links { get; set; }
        public bool Status { get; set; }
        public int? RelatedProductId { get; set; }
        public IFormFile MainImage { get; set; }
        public string MainImagePath { get; set; }

    }
}
