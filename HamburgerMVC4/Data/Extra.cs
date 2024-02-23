namespace HamburgerMVC4.Data
{
    public class Extra
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double UnitPrice { get; set; }
        public List<Order> Orders { get; set; } = new();
        public override string ToString()
        {
            return Name;
        }
    }
}
