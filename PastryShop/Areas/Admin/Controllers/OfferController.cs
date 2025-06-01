using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;
using PastryShop.Models.ViewModels;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            var viewModel = new OfferViewModel
            {
                Offers = await _context.Offers
                    .Include(o => o.Product)
                    .ToListAsync(),
                Products = await GetActiveProducts()
            };
            return View("~/Areas/Admin/Views/Offer/Index.cshtml", viewModel);
        }

        // Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new OfferViewModel
            {
                Products = await GetActiveProducts()
            };
            return View(viewModel);
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

            var viewModel = new OfferViewModel
            {
                Products = await GetActiveProducts()
            };
            return View(viewModel);
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