using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.Models
{
    public class Tokens
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime RegDateTime { get; set; }
        public bool Used { get; set; }
    }
}
