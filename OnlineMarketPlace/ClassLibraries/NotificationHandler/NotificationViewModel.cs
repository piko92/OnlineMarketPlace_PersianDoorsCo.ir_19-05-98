using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.NotificationHandler
{
    public class NotificationViewModel
    {
        public bool Status { get; set; } = true; //true , false
        public string Subject { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string ColorCode { get; set; } //success:#06d79c - error:#ef5350
        public string Icon { get; set; } // 'info' , 'warning' , 'error' , 'success'('notice')

        public string Position { get; set; } = "top-left"; //'top-left' ('tl') - top-right ('tr')
        public int? HideAfter { get; set; } = 10000; //time out must be mention here
        public int? Stack { get; set; } = 6;
    }
}
