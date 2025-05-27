namespace PastryShop.Models
{
    public class Footer
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? WorkingDays { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}