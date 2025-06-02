using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;
using PastryShop.Models.ViewModels;

namespace PastryShop.Controllers;
public class HomeController : Controller
{

    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }


    
    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeIndexViewModel
        {
            Sliders = await _context.Sliders
                .Where(s => s.IsActive)
                .OrderBy(s => s.Order)
                .ToListAsync(),

            Offers = await _context.Offers
                .Include(o => o.Product)
                .Where(o => o.IsActive && o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now)
                .Take(2)
                .ToListAsync(),

            Categories = await _context.Categories
                .Where(c => c.IsActive)
                .ToListAsync(),

            Products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .ToListAsync(),

            About = await _context.Abouts.FirstOrDefaultAsync(),

            ApprovedComments = await _context.Comments
                .Include(c => c.Product)
                .Where(c => c.IsApproved)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync()
        };

        return View(viewModel);
    }



    [Route("About")]
    public async Task<IActionResult> About()
    {
        var about = await _context.Abouts.FirstOrDefaultAsync();

        return View(about);
    }

    [Route("Menu")]
    public async Task<IActionResult> Menu()
    {
        var viewModel = new MenuViewModel
        {
            Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync(),
            Products = await _context.Products.Include(p => p.Category).Where(p => p.IsActive).ToListAsync()
        };
        return View(viewModel);
    }

    [Route("Order")]
    public IActionResult Order()
    {
        return View(new Order());
    }
    [HttpPost]
    [Route("Order")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OrderCreate(Order order)
    {
        if (ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;
            order.Status = OrderStatus.Beklemede;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Siparişiniz başarıyla alındı. En kısa sürede sizinle iletişime geçeceğiz.";
            return RedirectToAction("Index", "Home");
        }
        return View(order);
    }
    [Route("FAQ")]
    public async Task<IActionResult> FAQ()
    {
        var faq = await _context.FAQs
            .Where(f => f.IsActive)
            .OrderBy(f => f.Order)
            .ToListAsync();


        return View(faq);
    }


    [HttpPost]
    [Route("AddComment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment([Bind("Name,CommentText,ProductId")] Comment comment, IFormFile? ImageFile)
    {
        if (ModelState.IsValid)
        {
            comment.CreatedAt = DateTime.Now;
            comment.IsApproved = false;
            comment.PhotoUrl = "/frontend-template/images/defaultUser.png"; // Varsayılan fotoğraf

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "comments");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                comment.PhotoUrl = "/uploads/comments/" + fileName;
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra yayınlanacaktır.";
            return RedirectToAction("Index", "Home");
        }

        TempData["Error"] = "Yorum gönderilirken bir hata oluştu.";
        return RedirectToAction("Index", "Home");
    }
}
