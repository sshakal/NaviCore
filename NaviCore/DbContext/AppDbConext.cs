namespace NaviCore.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using NaviCore.Entities;
    public class AppDbContext : DbContext
    {
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Car> Cars { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи CarBrand -> Car (один ко многим)
            modelBuilder.Entity<CarBrand>()
                .HasMany(b => b.Cars)       // У CarBrand есть много Cars
                .WithOne(c => c.Brand)       // У Car есть один Brand
                .HasForeignKey(c => c.Brand.Id)  // Внешний ключ в Car
                .OnDelete(DeleteBehavior.Cascade); // Удаление Car при удалении CarBrand

            // Опционально: настройка индексов
            modelBuilder.Entity<CarBrand>()
                .HasIndex(b => b.BrandName)
                .IsUnique();  // Уникальность имени бренда

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.Name);
        }
    }
}
