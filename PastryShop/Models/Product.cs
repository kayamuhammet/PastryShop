namespace PastryShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category{ get; set; }
        public bool IsActive { get; set; } = true;
    }
}