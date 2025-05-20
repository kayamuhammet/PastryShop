namespace PastryShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation Property
        public ICollection<Product>? Products { get; set; }
    }
}