using System.ComponentModel.DataAnnotations;

namespace PastryShop.Models
{
    public class Slider
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur")]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public string BorderColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}