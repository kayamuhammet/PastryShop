namespace PastryShop.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Slider>? Sliders { get; set; }
        public List<Offer>? Offers { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public About? About { get; set; }
        public List<Comment>? ApprovedComments { get; set; }
    }
}