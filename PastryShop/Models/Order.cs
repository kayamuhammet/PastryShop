using System.ComponentModel.DataAnnotations;

namespace PastryShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [Display(Name = "Ad Soyad")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^[0-9]{4} [0-9]{3} [0-9]{2} [0-9]{2}$", ErrorMessage = "Telefon numarası formatı: 0XXX XXX XX XX şeklinde olmalıdır.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Pasta detayları zorunludur.")]
        [Display(Name = "Pasta Detayları")]
        public string? CakeDetails { get; set; }

        [Required(ErrorMessage = "Kişi sayısı zorunludur.")]
        [Display(Name = "Kişi Sayısı")]
        [Range(2, 10, ErrorMessage = "Kişi sayısı 2 ile 10 arasında olmalıdır.")]
        public int PersonCount { get; set; }

        [Required(ErrorMessage = "Teslim tarihi zorunludur.")]
        [Display(Name = "Teslim Tarihi")]
        public DateTime DeliveryDate { get; set; }

        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
    public enum OrderStatus
{
    Beklemede,
    Onaylandı,
    Hazırlanıyor,
    Tamamlandı,
    İptalEdildi
}
}