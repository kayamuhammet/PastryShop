using Microsoft.AspNetCore.Mvc.Rendering;

namespace PastryShop.Models.ViewModels
{
    public class OfferViewModel
    {
        public IEnumerable<Offer>? Offers { get; set; }
        public List<SelectListItem>? Products { get; set; }
    }
}