namespace HamburgerMVC4.Data
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public List<Order> Orders { get; set; } = new();
    }
}
