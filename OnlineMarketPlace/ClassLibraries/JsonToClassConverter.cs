using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.MessageHandler
{
    public class JsonToClassConverter : JsonConverter
    {
        private readonly Dictionary<string, string> _propertyMappings = new Dictionary<string, string>
        {
            {"IdCode", "Id" },
            {"Code","StatusCode" },
            {"Name","Title" },
            {"Description","Content" },
            {"IsError","IsError" },
            {"FilePath","Path" }
        };
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                if (!_propertyMappings.TryGetValue(jp.Name, out var name))
                    name = jp.Name;

                PropertyInfo prop = props.FirstOrDefault(pi =>
                    pi.CanWrite && pi.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == name);

                prop?.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
            }
            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Read and parse an external JSON file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static List<string> ReadJsonFile(string path, string token)
        {
            List<string> JsonArrayCells = new List<string>();
            string JSON_File = System.IO.File.ReadAllText(path);
            JToken jToken = JToken.Parse(JSON_File);
            JArray jArray = (JArray)jToken.SelectToken(token);
            foreach (JToken t in jArray)
            {
                string SToken = t.ToString();
                JsonArrayCells.Add(SToken);
            }
            return JsonArrayCells;
        }
    }
    
}
