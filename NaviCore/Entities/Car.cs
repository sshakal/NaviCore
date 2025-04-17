namespace NaviCore.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public CarBrand Brand { get; set; }
        public string Name { get; set; }
        public string Years { get; set; }
        public string Description { get; set; }
    }
}