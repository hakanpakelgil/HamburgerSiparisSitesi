using HamburgerMVC4.Data;
using System.ComponentModel.DataAnnotations;

namespace HamburgerMVC4.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int MenuId { get; set; }      

        public int OrderId { get; set; }

        [Required]
        public int DrinkId { get; set; }

        public int Quantity { get; set; } = 1;

        public Size Size { get; set; }

        public List<bool> ExtraBools { get; set; } = new();
        public double? Price { get;  set; }
        public List<Extra>? Extras { get; set; }
    }
}
