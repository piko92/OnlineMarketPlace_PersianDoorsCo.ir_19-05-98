using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class UserDetailViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRole { get; set; }
        public string UserRoleId { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public int? Rank { get; set; }
        public DateTime RegisteredDateTime { get; set; }
        public bool SpecialUser { get; set; }
        public bool Status { get; set; }
        public string DefinedByUserId { get; set; }
        public int? Gendre { get; set; }

        public byte[] Image { get; set; }
        public byte[] ThumbnailImage { get; set; }
    }
}
