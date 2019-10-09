using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class EditPurchuseCartStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public bool Approved { get; set; }
        public bool Sent { get; set; }
        public bool Delivered { get; set; }
    }
}
