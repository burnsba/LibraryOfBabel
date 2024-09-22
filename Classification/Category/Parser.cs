using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Classification.Json;

namespace Classification.Category
{
    public class Parser
    {
        private static bool _isInit = false;

        private static List<Category> _categoryCache = new List<Category>();

        public Parser()
        {
            Init();
        }

        public CategoryItem Parse(string text, bool isCaseSensitive = false, bool useAbbreviations = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return CategoryItem.Unclassified;
            }

            string compareText = text;

            if (!isCaseSensitive)
            {
                compareText = text.ToLower();
            }

            foreach (var cat in _categoryCache)
            {
                foreach (var item in cat.Items)
                {
                    if (compareText == item.Name)
                    {
                        return item;
                    }

                    if (isCaseSensitive)
                    {
                        if (item.AlternativeNames.IndexOf(compareText) > -1)
                        {
                            return item;
                        }

                        if (useAbbreviations)
                        {
                            if (item.Abbreviations.IndexOf(compareText) > -1)
                            {
                                return item;
                            }
                        }
                    }
                    else
                    {
                        foreach (var altn in item.AlternativeNames)
                        {
                            if (string.Compare(compareText, altn, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                return item;
                            }
                        }

                        if (useAbbreviations)
                        {
                            foreach (var abv in item.Abbreviations)
                            {
                                if (string.Compare(compareText, abv, StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    return item;
                                }
                            }
                        }
                    }
                }
            }

            return CategoryItem.Unclassified;
        }

        private void Init()
        {
            if (_isInit)
            {
                return;
            }

            _isInit = true;

            var assembly = Assembly.GetExecutingAssembly();
            var prefix = "Classification.DefaultSource";

            var categoryFiles = assembly.GetManifestResourceNames().Where(x => x.StartsWith(prefix));

            List<JsonCategory> missingParent = new List<JsonCategory>();

            foreach (var file in categoryFiles)
            {
                using (Stream stream = assembly.GetManifestResourceStream(file)!)
                using (StreamReader reader = new StreamReader(stream!))
                {
                    var jc = JsonConvert.DeserializeObject<JsonCategory>(reader.ReadToEnd());

                    if (ReferenceEquals(jc, null))
                    {
                        throw new NullReferenceException();
                    }

                    if (!string.IsNullOrEmpty(jc.Parent))
                    {
                        missingParent.Add(jc);
                    }

                    var cat = new Category(jc);
                    _categoryCache.Add(cat);
                }
            }

            foreach (var jc in missingParent)
            {
                var parent = _categoryCache.FirstOrDefault(x => x.CategoryName == jc.Parent);
                if (!ReferenceEquals(null, parent))
                {
                    var self = _categoryCache.FirstOrDefault(x => x.CategoryName == jc.CategoryName);
                    if (!ReferenceEquals(null, self))
                    {
                        self.Parent = parent;
                    }
                }
            }
        }
    }
}
