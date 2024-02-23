using Microsoft.AspNetCore.Identity;

namespace HamburgerMVC4.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;      
        public int DrinkId { get; set; }
        public Drink Drink { get; set; } = null!;        
        public Size Size { get; set; }
        public DateTime Date { get; set; }
        public List<Extra> Extras { get; set; } = new();
        public string? Cost => Price.ToString("c2");
        public double Price { get; set; }
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}
