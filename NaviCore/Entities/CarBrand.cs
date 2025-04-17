namespace NaviCore.Entities
{
    public class CarBrand
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}
