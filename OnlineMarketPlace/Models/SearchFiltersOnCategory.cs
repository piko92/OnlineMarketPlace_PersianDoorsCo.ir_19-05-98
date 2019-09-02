using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class SearchFiltersOnCategory
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? SearchFiltersId { get; set; }
        public bool Status { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SearchFiltersId")]
        public virtual SearchFilters SearchFilters { get; set; }
    }
}
