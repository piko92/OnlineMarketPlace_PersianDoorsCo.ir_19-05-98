using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class UserPanelViewModel
    {
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public EditUserViewModel EditUserViewModel { get; set; }
        public AddressViewModel AddressViewModel { get; set; }
    }
}
