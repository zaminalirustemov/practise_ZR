using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boutique_ZR.Models
{
    public class BoutiqueDbContext:IdentityDbContext
    {
        public BoutiqueDbContext(DbContextOptions<BoutiqueDbContext> options):base(options) {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
