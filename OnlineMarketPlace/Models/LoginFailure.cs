using OnlineMarketPlace.Repository;
using System;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public partial class LoginFailure : IEntity<int>
    {
        //public LoginFailure()
        //{
        //    BlockedUser = new HashSet<BlockedUser>();
        //}

        public int Id { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public bool Mobile { get; set; }
        public string Os { get; set; }
        public string Comment { get; set; }
        public string EnteredUserName { get; set; }
        public string EnteredPassword { get; set; }
        public DateTime? RegDateTime { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<BlockedUser> BlockedUser { get; set; }
    }
}
