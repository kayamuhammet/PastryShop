using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        // Get
        public async Task<IActionResult> Index()
        {
            return View("~/Areas/Admin/Views/Slider/Index.cshtml", await _context.Sliders.OrderBy(s => s.Order).ToListAsync());
        }

        // Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Sliders.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }


        // Edit

        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slider slider, IFormFile? ImageFile)
        {
            if(ModelState.IsValid)
            {
                var existingProduct = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(p => p.Id == slider.Id);

                if (existingProduct == null)
                    return NotFound();

                _context.Update(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }


        // Delete

        public async Task<IActionResult> Delete(int? id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if(slider == null)
                return NotFound();
            
            return View(slider);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if(slider == null)
                return NotFound();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}