using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FoodApplication.Models;

namespace FoodApplication.Data
{
    public class FoodDBContext : IdentityDbContext<ApplicationUser>
    {
        public FoodDBContext(DbContextOptions<FoodDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
