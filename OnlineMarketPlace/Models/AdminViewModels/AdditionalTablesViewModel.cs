using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class AdditionalTablesViewModel
    {
        public IFormFile Country { get; set; }
        public bool RewriteCountry { get; set; }

        public IFormFile Province { get; set; }
        public bool RewriteProvince { get; set; }

        public IFormFile City { get; set; }
        public bool RewriteCity { get; set; }
    }
}
