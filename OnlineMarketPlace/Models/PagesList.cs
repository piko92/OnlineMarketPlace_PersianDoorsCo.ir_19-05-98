using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class PagesList : IEntity<int>
    {
        //public PagesList()
        //{
        //    Banner = new HashSet<Banner>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Link { get; set; }
        public bool Status { get; set; }

        public int? GeneralPagesId { get; set; }
        [ForeignKey("GeneralPagesId")]
        public virtual GeneralPage GeneralPages { get; set; }

        public virtual ICollection<Banner> Banner { get; set; }
    }
}
