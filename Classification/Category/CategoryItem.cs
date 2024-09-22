using Classification.Json;

namespace Classification.Category
{
    public class CategoryItem
    {
        public static CategoryItem Unclassified { get; } = new CategoryItem(Category.Unclassified) { Name = "Unclassified" };

        public Category Parent { get; private set; }

        public string Name { get; set; }

        public List<string> AlternativeNames { get; set; } = new List<string>();

        public List<string> Abbreviations { get; set; } = new List<string>();

        public CategoryItem(Category parent)
        {
            Parent = parent;
        }

        internal CategoryItem(Category parent, JsonCategoryItem jci)
        {
            Parent = parent;
            Name = jci.Name;

            if (!ReferenceEquals(null, jci.AlternativeNames))
            {
                foreach (var x in jci.AlternativeNames)
                {
                    AlternativeNames.Add(x);
                }
            }

            if (!ReferenceEquals(null, jci.Abbreviations))
            {
                foreach (var x in jci.Abbreviations)
                {
                    Abbreviations.Add(x);
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}