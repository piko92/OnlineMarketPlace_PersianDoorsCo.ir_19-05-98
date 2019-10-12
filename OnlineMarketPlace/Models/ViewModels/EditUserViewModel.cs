using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IFormFile img { get; set; }
        public string Phonenumber { get; set; }
        public string Nationalcode { get; set; }
        public DateTime? Dateofbirth { get; set; }

        public int Dateofbirth_Day { get; set; }
        public int Dateofbirth_Month { get; set; }
        public int Dateofbirth_Year { get; set; }

        public bool? Gender { get; set; }
        public bool Specialuser { get; set; }
        public bool Status { get; set; }
    }
}
