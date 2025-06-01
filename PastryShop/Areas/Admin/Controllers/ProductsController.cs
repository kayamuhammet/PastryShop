using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;
using PastryShop.Models.ViewModels;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            var viewModel = new ProductListViewModel
            {
                Products = await _context.Products.Include(p => p.Category).ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View("~/Areas/Admin/Views/Products/Index.cshtml", viewModel);
        }

        // Create
        public IActionResult Create()
        {
            var viewModel = new ProductListViewModel
            {
                Products = new List<Product> { new Product() },
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
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

            var viewModel = new ProductListViewModel
            {
                Products = new List<Product> { product },
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var viewModel = new ProductListViewModel
            {
                Products = new List<Product> { product },
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
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
                    product.Image = existingProduct.Image;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductListViewModel
            {
                Products = new List<Product> { product },
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
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