using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.AdminViewModels
{
    public class SettingViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? BaseCurrencyId { get; set; }
        public bool IsStatusActive { get; set; }
        public bool IsMarketPlace { get; set; }
        public bool Status { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string AdminEmail { get; set; }
        public string AdminEmailPassword { get; set; }
        public string EmailPort { get; set; }
        public string EmailProtocol { get; set; }
        public string EmailServiceProvider { get; set; }
        public string SMSUsername { get; set; }
        public string SMSPassword { get; set; }
        public string SMSApiAddress { get; set; }
        public string SMSApiNumber { get; set; }
    }
}
