using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();
            return View("~/Areas/Admin/Views/Order/Index.cshtml", orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
                return NotFound();

            return View("~/Areas/Admin/Views/Order/Details.cshtml", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Beklemede;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Siparişiniz başarıyla alındı. En kısa sürede sizinle iletişime geçeceğiz.";
                return View("~/Views/Home/Order.cshtml");
            }
            return View("~/Views/Home/Order.cshtml", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(OrderStatus orderStatus, int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            order.Status = orderStatus;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}