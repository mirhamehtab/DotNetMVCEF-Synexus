namespace DotNetMVCEF.Models.Entities
{
    public class Product // issko daikh k db and tables will be created in SSMS after migrations
    {
        public int Id {  get; set; } //auto-incremented
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }  //float->real in SSMS
    }
} // cant use this as it is kyuk iss ki id auto-increment hai iss liye we have to add another model that'll be used base for form
