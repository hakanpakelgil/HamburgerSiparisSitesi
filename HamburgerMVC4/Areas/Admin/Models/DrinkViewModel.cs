using HamburgerMVC4.Attributes;
using HamburgerMVC4.Data;

namespace HamburgerMVC4.Areas.Admin.Models
{
    public class DrinkViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;

        [ValidImage]
        public IFormFile? Image { get; set; } = null!;

        public string? ImagePath { get; set; } = null!;
    }
}
