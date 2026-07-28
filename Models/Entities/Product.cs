namespace DotNetMVCEF.Models.Entities
{
    public class Product // issko daikh k db and tables will be created in SSMS after migrations
    {
        public int Id {  get; set; } //auto-incremented
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }  //float->real in SSMS
        public int? CategoryId { get; set; }   // foreign key
        public Category? Category { get; set; }  // navigation property, ? isliye kyunki EF khud fill karega
        public string? ImagePath { get; set; }   // nullable -- existing products ke paas image nahi hogi

    }
} 
