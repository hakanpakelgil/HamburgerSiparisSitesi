namespace HamburgerMVC4.Data
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double UnitPrice { get; set; }
        public string Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; }     
        public List<Order> Orders { get; set; }
    }


}
