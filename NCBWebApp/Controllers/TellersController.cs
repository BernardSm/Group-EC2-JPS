using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCBWebApp.Data;
using NCBWebApp.Models;

namespace NCBWebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class TellersController : Controller
    {
        private readonly NCBWebAppContext _context;

        public TellersController(NCBWebAppContext context)
        {
            _context = context;
        }

        // GET: Tellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teller.ToListAsync());
        }

        // GET: Tellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teller = await _context.Teller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teller == null)
            {
                return NotFound();
            }

            return View(teller);
        }

        // GET: Tellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,PhoneNumber")] Teller teller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teller);
        }

        // GET: Tellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teller = await _context.Teller.FindAsync(id);
            if (teller == null)
            {
                return NotFound();
            }
            return View(teller);
        }

        // POST: Tellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,PhoneNumber")] Teller teller)
        {
            if (id != teller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TellerExists(teller.Id))
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
            return View(teller);
        }

        // GET: Tellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teller = await _context.Teller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teller == null)
            {
                return NotFound();
            }

            return View(teller);
        }

        // POST: Tellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teller = await _context.Teller.FindAsync(id);
            _context.Teller.Remove(teller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TellerExists(int id)
        {
            return _context.Teller.Any(e => e.Id == id);
        }
    }
}
