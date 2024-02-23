using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerMVC4.Data;
using HamburgerMVC4.Areas.Admin.Models;
using static System.Net.Mime.MediaTypeNames;
using HamburgerMVC4.Areas.Admin.Data;

namespace HamburgerMVC4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Menu _menu;

        public MenusController(ApplicationDbContext context, Menu menu)
        {
            _menu = menu;
            _context = context;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.Include(m => m.Category).ToListAsync());
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UnitPrice,Image,CategoryId")] MenuViewModel menuViewModel)
        {
            if (menuViewModel != null)
            {
                if (ModelState.IsValid && menuViewModel.Image != null)
                {

                    _menu.Name = menuViewModel.Name;
                    _menu.Image = ImageHandler.CreateImage(menuViewModel.Image,"menus");
                    _menu.UnitPrice = menuViewModel.UnitPrice;
                    _menu.Description = menuViewModel.Description;
                    _menu.CategoryId = menuViewModel.CategoryId;
                    _context.Add(_menu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                ViewBag.ErrorMessage = "Please select a image!";
                ViewBag.Categories = _context.Categories.ToList();
                return View(menuViewModel);
            }
            else return NotFound();

        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            TempData["Id"] = id;
            if (menu == null)
            {
                return NotFound();
            }
            MenuViewModel menuViewModel = new()
            {
                Description = menu.Description,
                Name = menu.Name,
                UnitPrice = menu.UnitPrice,
                CategoryId = menu.CategoryId,
            };
            ViewBag.Image = menu.Image;
            ViewBag.Categories = _context.Categories.ToList();
            return View(menuViewModel);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,UnitPrice,Image,CategoryId")] MenuViewModel menuViewModel)
        {
            Menu updated = _context.Menus.Find(TempData["Id"]);

            if (updated == null)
            {
                return NotFound();
            }
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                try
                {
                    if (menuViewModel.Image != null)
                    {                      
                        updated.Image = ImageHandler.EditImage(menuViewModel.Image,"menus", updated.Image);
                    }

                    updated.UnitPrice = menuViewModel.UnitPrice;
                    updated.Description = menuViewModel.Description;
                    updated.Name = menuViewModel.Name;
                    updated.CategoryId = menuViewModel.CategoryId;

                    _context.Update(updated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(updated.Id))
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
            ViewBag.Categories = _context.Categories.ToList();
            return View(menuViewModel);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            string file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", menu.Image);
            System.IO.File.Delete(file);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }

    }
}
