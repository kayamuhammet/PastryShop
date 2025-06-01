using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FAQController : Controller
    {
        private readonly AppDbContext _context;

        public FAQController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _context.FAQs.OrderBy(f => f.Order).ToListAsync();
            return View("~/Areas/Admin/Views/FAQ/Index.cshtml",faqs);
        }

        // Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ faq)
        {
            if(ModelState.IsValid)
            {
                _context.Add(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(faq);
        }



        // Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return NotFound();
            
            var faq = await _context.FAQs.FindAsync(id);

            if(faq == null)
                return NotFound();
            
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FAQ faq, int id)
        {
            if(id != faq.Id)
                return NotFound();
            
            if(ModelState.IsValid)
            {
                _context.Update(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _context.FAQs.FindAsync(id);

            if(faq != null)
            {
                _context.FAQs.Remove(faq);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}