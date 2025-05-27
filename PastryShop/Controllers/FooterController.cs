using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Controllers
{
    public class FooterController : Controller
    {
        private readonly AppDbContext _context;
        public FooterController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var footer = await _context.Footers.FirstOrDefaultAsync();
            if (footer == null)
            {
                footer = new Footer();
            }
            return View("~/Views/Admin/Footer/Index.cshtml", footer);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Footer footer)
        {
            if (ModelState.IsValid)
            {
                if (footer.Id == 0)
                {
                    _context.Footers.Add(footer);
                }
                else
                {
                    _context.Update(footer);
                }
                await _context.SaveChangesAsync();
                TempData["Success"] = "Footer bilgileri başarıyla kaydedildi.";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Footer/Index.cshtml", footer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var footer = await _context.Footers.FindAsync(id);
            if (footer != null)
            {
                _context.Footers.Remove(footer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Footer bilgileri başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
