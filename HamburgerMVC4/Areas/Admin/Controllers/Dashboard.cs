using HamburgerMVC4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HamburgerMVC4.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _db;

        public Dashboard(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Revenue = _db.Orders.Sum(x => x.Price).ToString("c2");
            ViewBag.Users = _db.Users.Count() - 1;
            ViewBag.Orders = _db.Orders.Count();
            ViewBag.DailyRevenue = _db.Orders.Where(x => x.Date == DateTime.Today).Sum(x => x.Price).ToString("c2");
            ViewBag.DailyOrders = _db.Orders.Where(x => x.Date == DateTime.Today).Count();

            var list = _db.Orders.Include(a => a.Menu).Include(b => b.User).Include(c => c.Drink).Include(c => c.Extras).OrderByDescending(a => a.Id)
                .ToList();

            var pagedItemList = list.Skip((page - 1) * 15).Take(15);

            ViewBag.PageNumber = page;
            ViewBag.TotalPages = Math.Ceiling((double)list.Count / 15);

            return View(pagedItemList.ToList());
        }
    }
}
