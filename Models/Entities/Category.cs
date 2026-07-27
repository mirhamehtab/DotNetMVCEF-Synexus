namespace DotNetMVCEF.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation property -- 1 catogory may have multiple prods
        public List<Product> Products { get; set; } = new List<Product>();
    }
}