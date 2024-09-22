using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Classification.Json
{
    internal class JsonCategory
    {
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("items")]
        public List<JsonCategoryItem> Items { get; set; }

        public override string ToString()
        {
            return CategoryName;
        }
    }
}
