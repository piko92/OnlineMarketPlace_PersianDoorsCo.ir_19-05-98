using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.Models
{
    public partial class SiteGeneralInfo
    {
        public int Id { get; set; }
        public string ShopTitle { get; set; }
        public string LatinShopTitle { get; set; }
        public string MetaTagJson { get; set; }
        public string MetaDescription { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string DescriptionForFooter { get; set; }
        public string CanonicalTag { get; set; }
        public string CanonicalTagDescription { get; set; }
        public byte[] PrimarySiteLogo { get; set; }
        public byte[] SecondarySiteLogo { get; set; }
        public string PrimarySiteLogoPath { get; set; }
        public string SecondarySiteLogoPath { get; set; }
        public string ConnectedLinks { get; set; }
        public string UserId { get; set; }
        public string Motto { get; set; }
        public string PublicEmail { get; set; }
        public string BusinessEmail { get; set; }
        public DateTime? RegDateTime { get; set; }
        public string PhoneNumbersList { get; set; }
        public string AddressesList { get; set; }
        public bool Status { get; set; }
        public string Signature { get; set; }
        public bool SetForMainPage { get; set; }
        public string LicensesListName { get; set; }
        public string AchievedAwards { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
