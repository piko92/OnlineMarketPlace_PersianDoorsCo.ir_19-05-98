using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using OnlineMarketPlace.Repository.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Subject: IEntity<int>, IEntityEx<string>
    {
        //public Subject()
        //{
        //    PhotoGallery = new HashSet<PhotoGallery>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PhotoGallery> PhotoGallery { get; set; }
    }
}
