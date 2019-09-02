using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.MessageHandler
{
    [JsonConverter(typeof(JsonToClassConverter))]
    public class Messages
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("StatusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("IsError")]
        public bool IsError { get; set; }
    }
}
