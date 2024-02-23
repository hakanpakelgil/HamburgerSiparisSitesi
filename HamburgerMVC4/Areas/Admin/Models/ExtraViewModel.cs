using HamburgerMVC4.Attributes;
using HamburgerMVC4.Data;

namespace HamburgerMVC4.Areas.Admin.Models
{
    public class ExtraViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImagePath { get; set; } = null!;
        public double UnitPrice { get; set; }
    }
}
