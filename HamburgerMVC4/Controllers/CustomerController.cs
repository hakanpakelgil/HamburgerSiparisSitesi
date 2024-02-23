using HamburgerMVC4.Areas.Admin.Controllers;
using HamburgerMVC4.Areas.Admin.Models;
using HamburgerMVC4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HamburgerMVC4.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Menu = await _db.Menus.FindAsync(id);
            ViewBag.Extras = await _db.Extras.ToListAsync();
            ViewBag.Drinks = await _db.Drinks.ToListAsync();
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.DrinkId == 0)
                {
                    ViewBag.DrinkError = "Please select a drink";
                    ViewBag.Menu = await _db.Menus.FindAsync(vm.MenuId);
                    ViewBag.Extras = await _db.Extras.ToListAsync();
                    ViewBag.Drinks = await _db.Drinks.ToListAsync();
                    return View(vm);
                }
                List<Extra> dbExtras = await _db.Extras.ToListAsync();
                List<Extra> extras = new List<Extra>();

                for (int i = 0; i < vm.ExtraBools.Count; i++)
                {
                    if (vm.ExtraBools[i])
                    {
                        extras.Add(dbExtras[i]);
                    }
                }

                double totalExtrasPrice = 0;
                if (extras.Count > 2)
                {
                    var extrasPrices = extras.OrderByDescending(u => u.UnitPrice).ToList().Take(extras.Count - 2);
                    totalExtrasPrice = extrasPrices.Sum(u => u.UnitPrice);
                }
                double menuPrice = _db.Menus.Find(vm.MenuId)!.UnitPrice;
                if (vm.Size == Size.Medium)
                {
                    menuPrice *= 1.1;
                }
                else if (vm.Size == Size.Large)
                {
                    menuPrice *= 1.3;
                }

                double totalPrice = menuPrice + totalExtrasPrice;

                vm.Price = totalPrice;
                vm.Extras = extras;

                TempData["Extras"] = JsonConvert.SerializeObject(vm.Extras);

                return RedirectToAction("OrderConfirmation", vm);

            }
            return View();
        }


        public async Task<IActionResult> OrderConfirmation(OrderViewModel vm)
        {

            var drink = await _db.Drinks.Where(e => e.Id == vm.DrinkId).ToListAsync();
            var menu = await _db.Menus.Where(a => a.Id == vm.MenuId).ToListAsync();
            var extraList = JsonConvert.DeserializeObject<List<Extra>>((string)TempData["Extras"]!);
            vm.Extras = extraList;

            ViewBag.Drink = drink;
            ViewBag.Menu = menu;
            ViewBag.Extras = extraList;

            TempData["OrderVm"] = JsonConvert.SerializeObject(vm);
            ViewBag.Extras = extraList;

            return View(vm);
        }


        public async Task<IActionResult> OrderSummary()
        {
            var Json = TempData["OrderVm"] as string;
            OrderViewModel vm = JsonConvert.DeserializeObject<OrderViewModel>(Json!)!;
            List<Extra> dbExtras = await _db.Extras.ToListAsync();
            List<Extra> extras = new List<Extra>();

            for (int i = 0; i < vm.ExtraBools.Count; i++)
            {
                if (vm.ExtraBools[i])
                {
                    extras.Add(dbExtras[i]);
                }
            }

            Order o = new Order()
            {
                MenuId = vm.MenuId,
                DrinkId = vm.DrinkId,
                Size = vm.Size,
                Extras = extras,
                Price = (double)vm.Price!,
                UserId = _userManager.GetUserId(User)!,
                Date = DateTime.Today
            };

            await _db.Orders.AddAsync(o);
            await _db.SaveChangesAsync();
            return View();
        }

        public IActionResult OrderHistory()
        {
            List<Order> orders = _db.Orders
                .Include(u => u.Menu)
                .Include(u => u.Drink)
                .Include(u => u.Extras)
                .Where(u => _userManager.GetUserId(User) == u.UserId)
                .ToList();

            return View(orders);
        }

        public IActionResult OrderInfo(int page = 1)
        {
            List<DateTime> dates = new();

            var distinctDates = _db.Orders
                .Where(a => a.UserId == _userManager.GetUserId(User))
                .Select(x => x.Date)
                .Distinct()
                .OrderByDescending(x => x.Date)
                .ToList();

            int numberOfDistinctDates = distinctDates.Count;
      

            if(_db.Orders.Include(a=>a.User).Where(a => a.UserId == _userManager.GetUserId(User)).ToList() != null)
            {
                var pagedItemList = _db.Orders.Include(a => a.Menu).Include(a => a.Extras).Include(a => a.Drink).Include(a => a.User).Where(a => a.UserId == _userManager.GetUserId(User)).Where(x => x.Date == distinctDates[page - 1].Date).OrderByDescending(a => a.Id).ToList();

                foreach (var i in distinctDates)
                {
                    dates.Add(i.Date);
                }
                ViewBag.PageNumber = page;
                ViewBag.TotalPages = numberOfDistinctDates;
                ViewBag.DistinctDates = dates;

                return View(pagedItemList);

            }                

           return View(_db.Orders.Include(a => a.User).Where(a => a.UserId == _userManager.GetUserId(User)).ToList());
        }

    }
}
