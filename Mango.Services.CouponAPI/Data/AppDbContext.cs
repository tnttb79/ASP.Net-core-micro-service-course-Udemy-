using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // seed the db with some data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(new Coupon { Id = 1, CouponCode = "10OFF", DiscountAmount = 10, MinAmount = 50 });
            modelBuilder.Entity<Coupon>().HasData(new Coupon { Id = 2, CouponCode = "20OFF", DiscountAmount = 20, MinAmount = 100 });
        }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
