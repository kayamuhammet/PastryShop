namespace PastryShop.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
    }
}