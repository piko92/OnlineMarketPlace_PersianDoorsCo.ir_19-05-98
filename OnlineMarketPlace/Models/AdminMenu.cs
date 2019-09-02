using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class AdminMenu : IEntity<int>
    {
        //public AdminMenu()
        //{
        //    InverseParent = new HashSet<AdminMenu>();
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string LatinTitle { get; set; }
        public string UserRoleId { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
        public int? ParentId { get; set; }
        public string IconPath { get; set; }
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public DateTime? RegDateTime { get; set; }

        [ForeignKey("ParentId")]
        public virtual AdminMenu Parent { get; set; }
        public virtual ICollection<AdminMenu> InverseParent { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

       // public virtual AspNetRoles UserRole { get; set; }
    }
}
