namespace veebipoe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
