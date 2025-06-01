namespace PastryShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? CommentText { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}