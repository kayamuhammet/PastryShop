using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PastryShop.Models;

namespace PastryShop.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}