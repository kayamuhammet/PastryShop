using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Controllers
{
    public class OfferController : Controller
    {
        private readonly AppDbContext _context;
        public OfferController(AppDbContext context)
        {
            _context = context;
        }



        private async Task<List<SelectListItem>> GetActiveProducts()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Title
                })
                .ToListAsync();
        }


        // Get
        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers
                .Include(o => o.Product)
                .ToListAsync();

            ViewBag.Products = await GetActiveProducts();
            return View("~/Views/Admin/Offer/Index.cshtml", offers);
        }

        // Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await GetActiveProducts();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Offer offer)
        {
            if (ModelState.IsValid)
            {

                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Tatlılar")
                .ToListAsync();


            ViewBag.Products = products;
            return View(offer);
        }


        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            
            if(offer == null)
                return NotFound();
            
            return View(offer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Offer offer, int id)
        {
            if(ModelState.IsValid)
            {
                var existingOffer = await _context.Offers.FindAsync(id);
                if(existingOffer == null)
                    return NotFound();
                

                existingOffer.StartDate = offer.StartDate;
                existingOffer.EndDate = offer.EndDate;
                existingOffer.IsActive = offer.IsActive;
            
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // Delete

        public async Task<IActionResult> Delete(int? id)
        {
            var offer = await _context.Offers
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (offer == null)
                return NotFound();

            return View(offer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}