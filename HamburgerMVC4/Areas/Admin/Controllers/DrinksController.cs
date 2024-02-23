using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerMVC4.Data;
using HamburgerMVC4.Areas.Admin.Models;

namespace HamburgerMVC4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Drink _drink;

        public DrinksController(ApplicationDbContext context, Drink drink)
        {
            _context = context;
            _drink = drink;
        }

        // GET: Admin/Drinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drinks.ToListAsync());
        }

        // GET: Admin/Drinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.Image = drink.Image;
            return View(drink);
        }

        // GET: Admin/Drinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,ImagePath")] DrinkViewModel drinkViewModel)
        {
            if (ModelState.IsValid)
            {
                _drink.Name = drinkViewModel.Name;
                if (drinkViewModel.Image != null)
                {
                    string ext = Path.GetExtension(drinkViewModel.Image.FileName);
                    string newFile = Guid.NewGuid() + ext;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "drinks", newFile);
                    using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                    {
                        drinkViewModel.Image.CopyTo(fs);
                    }
                    _drink.Image = newFile;
                    _context.Add(_drink);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.ErrorMessage = "Please select a image";
            return View(drinkViewModel);
        }

        // GET: Admin/Drinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            DrinkViewModel drinkVm = new()
            {
                Id = drink.Id,
                Name = drink.Name,
                ImagePath = drink.Image
            };
            return View(drinkVm);
        }

        // POST: Admin/Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Image,ImagePath")] DrinkViewModel drinkVm)
        {
            Drink updated = _context.Drinks.Find(drinkVm.Id)!;
            if (updated == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    updated.Name = drinkVm.Name;
                    if (drinkVm.Image != null)
                    {
                        string oldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "drinks", updated.Image);
                        System.IO.File.Delete(oldFile);
                        string ext = Path.GetExtension(drinkVm.Image.FileName);
                        string newFile = Guid.NewGuid() + ext;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "drinks", newFile);
                        using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                        {
                            drinkVm.Image.CopyTo(fs);
                        }
                        updated.Image = newFile;
                    }
                    _context.Update(updated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists((int)drinkVm.Id!))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(drinkVm);
        }

        // GET: Admin/Drinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        // POST: Admin/Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "drinks", drink.Image);
                System.IO.File.Delete(path);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
    }
}
