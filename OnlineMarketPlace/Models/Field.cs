using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class Field : IEntity<int>
    {
        //public Field()
        //{
        //    Category = new HashSet<Category>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
        public string Tags { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }
}
