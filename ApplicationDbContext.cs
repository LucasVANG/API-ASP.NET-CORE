using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration Primary keys
            modelBuilder.Entity<Sale>().HasKey(s => s.Sale_id);
            modelBuilder.Entity<Shop>().HasKey(sh => sh.Shop_id);
            modelBuilder.Entity<Region>().HasKey(r => r.Region_id);
            modelBuilder.Entity<Product>().HasKey(p => p.Product_id);




            
            // Configuration pour assurer le passage de datetime à DATE
            modelBuilder.Entity<Sale>()
                .Property(s => s.Sale_date)
                .HasColumnType("DATE");

            base.OnModelCreating(modelBuilder);
        }
    }
}
