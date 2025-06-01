using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }


        // Get
        public async Task<IActionResult> Index()
        {
            return View("~/Areas/Admin/Views/About/Index.cshtml", await _context.Abouts.ToListAsync());
        }

        // Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/abouts", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                about.ImageUrl = "/uploads/abouts/" + fileName;

                _context.Abouts.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(about);
        }

        // Edit

        public async Task<IActionResult> Edit(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
                return NotFound();
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(About about, IFormFile? ImageFile)
        {
            if(ModelState.IsValid)
            {
                var existingProduct = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == about.Id);

                if (existingProduct == null)
                    return NotFound();

                if (ImageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/abouts", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    DeleteOldImage(existingProduct.ImageUrl);

                    about.ImageUrl = "/uploads/abouts/" + fileName;
                }
                else
                {
                    about.ImageUrl = existingProduct.ImageUrl;
                }

                _context.Update(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(about);
        }



        // Delete

        public async Task<IActionResult> Delete(int? id)
        {
            var about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            if(about == null)
                return NotFound();
            
            return View(about);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if(about == null)
                return NotFound();
            
            DeleteOldImage(about.ImageUrl);

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        private void DeleteOldImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }




    }


}