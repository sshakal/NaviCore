using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NaviCore.DbContext;
using System.Text.Json;

namespace NaviCore.Services
{
    public class CacheInitializer
    {
        private readonly IDistributedCache _cache;
        private readonly AppDbContext _dbContext;

        public CacheInitializer(IDistributedCache cache, AppDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            // Загружаем все бренды с машинами (Eager Loading)
            var carBrands = await _dbContext.CarBrands
                .Include(b => b.Cars)
                .ToListAsync();

            // Сериализуем в JSON и сохраняем в Redis
            foreach (var brand in carBrands)
            {
                var brandKey = $"brand:{brand.Id}";
                var brandJson = JsonSerializer.Serialize(brand);
                await _cache.SetStringAsync(brandKey, brandJson);
            }

            // Можно также сохранить отдельно все машины
            var allCars = carBrands.SelectMany(b => b.Cars).ToList();
            foreach (var car in allCars)
            {
                var carKey = $"car:{car.Id}";
                var carJson = JsonSerializer.Serialize(car);
                await _cache.SetStringAsync(carKey, carJson);
            }
        }
    }
}
