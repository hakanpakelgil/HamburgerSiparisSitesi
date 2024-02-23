using HamburgerMVC4.Attributes;
using HamburgerMVC4.Data;

namespace HamburgerMVC4.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double UnitPrice { get; set; }
        public int CategoryId { get; set; }

        [ValidImage]
        public IFormFile? Image { get; set; }
    }
}
