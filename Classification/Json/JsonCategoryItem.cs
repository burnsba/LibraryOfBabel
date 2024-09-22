using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Classification.Json
{
    internal class JsonCategoryItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alt")]
        public List<string> AlternativeNames { get; set; }

        [JsonProperty("abv")]
        public List<string> Abbreviations { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
