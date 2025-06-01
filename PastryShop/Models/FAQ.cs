namespace PastryShop.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        public string? Question { get; set; }

        public string? Answer { get; set; }
        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}