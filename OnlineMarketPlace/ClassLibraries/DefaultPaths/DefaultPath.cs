using Newtonsoft.Json;
using OnlineMarketPlace.ClassLibraries.MessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.DefaultPaths
{
    [JsonConverter(typeof(JsonToClassConverter))]
    public class DefaultPath
    {
        [JsonProperty("Title")]
        public string Name { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
    }
}
