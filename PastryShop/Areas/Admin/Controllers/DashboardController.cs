using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Data;
using PastryShop.Models.ViewModels;

namespace PastryShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(-i))
                .Reverse()
                .ToList();

            var allOrders = await _context.Orders.ToListAsync();

            // Her gün için sipariş sayısını hesaplar
            var orderCounts = last7Days
                .Select(date => allOrders.Count(o => o.OrderDate.Date == date))
                .ToList();

            var viewModel = new DashboardViewModel
            {
                TotalOrders = allOrders.Count,
                TotalProducts = await _context.Products.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                Last7DaysOrders = allOrders.Count(o => o.OrderDate >= DateTime.Now.AddDays(-7)),
                OrderChartLabels = last7Days.Select(d => d.ToString("dd.MM.yyyy")).ToList(),
                OrderChartData = orderCounts
            };

            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml", viewModel);
        }
    }
}