using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // Get
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View("~/Views/Admin/Products/Index.cshtml", products);
        }

        // Create

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.Image = "/uploads/" + fileName;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);

                if (existingProduct == null)
                    return NotFound();

                if (ImageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    
                    DeleteOldImage(existingProduct.Image);

                    product.Image = "/uploads/" + fileName;
                }
                else
                {
                    // Eğer yeni resim yüklendiğinde hata olursa, mevcut resmi koru
                    product.Image = existingProduct.Image;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // Delete

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            
            DeleteOldImage(product.Image);

            
            _context.Products.Remove(product);
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