using Classification.Json;

namespace Classification.Category
{
    public class Category
    {
        public static Category Unclassified { get; } = new Category() { CategoryName = "Unclassified" };

        public Category? Parent { get; set; }

        public string CategoryName { get; set; }

        public List<CategoryItem> Items { get; set; } = new List<CategoryItem>();

        public Category()
        { }

        internal Category(JsonCategory jc)
        {
            CategoryName = jc.CategoryName;

            foreach (var x in jc.Items)
            {
                Items.Add(new CategoryItem(this, x));
            }
        }

        public override string ToString()
        {
            return CategoryName;
        }
    }
}