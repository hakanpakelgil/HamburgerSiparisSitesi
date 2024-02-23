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
    public class ExtrasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Extra _extra;

        public ExtrasController(ApplicationDbContext context, Extra extra)
        {
            _context = context;
            _extra = extra;
        }

        // GET: Admin/Extras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Extras.ToListAsync());
        }

        // GET: Admin/Extras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // GET: Admin/Extras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice")] ExtraViewModel extraVm)
        {
            if (ModelState.IsValid)
            {
                _extra.Name = extraVm.Name;
                _extra.UnitPrice = extraVm.UnitPrice;
            
                
                    _context.Add(_extra);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
            }         
            return View(extraVm);
        }

        // GET: Admin/Extras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras.FindAsync(id);
            if (extra == null)
            {
                return NotFound();
            }
            ExtraViewModel extraVm = new()
            {
                Id = extra.Id,
                Name = extra.Name,
                UnitPrice = extra.UnitPrice,            
            };
            return View(extraVm);
        }

        // POST: Admin/Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,UnitPrice")] ExtraViewModel extraVm)
        {
            Extra updated = _context.Extras.Find(extraVm.Id)!;
            if (updated == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    updated.Name = extraVm.Name;
                    updated.UnitPrice = extraVm.UnitPrice;
                                      
                    
                    _context.Update(updated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraExists((int)extraVm.Id!))
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
            return View(extraVm);
        }

        // GET: Admin/Extras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Admin/Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var extra = await _context.Extras.FindAsync(id);
            if (extra != null)
            {
                _context.Extras.Remove(extra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraExists(int id)
        {
            return _context.Extras.Any(e => e.Id == id);
        }
    }
}
