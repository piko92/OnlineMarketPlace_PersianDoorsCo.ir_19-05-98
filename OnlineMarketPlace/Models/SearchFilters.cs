using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class SearchFilters
    {
        //public SearchFilters()
        //{
        //    SearchFiltersOnCategory = new HashSet<SearchFiltersOnCategory>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string AliasName { get; set; }
        public string Comment { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string UserId { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<SearchFiltersOnCategory> SearchFiltersOnCategory { get; set; }
    }
}
