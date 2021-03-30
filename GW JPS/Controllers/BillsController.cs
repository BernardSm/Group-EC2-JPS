using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GW_JPS.Data;
using GW_JPS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GW_JPS.ViewModels;

namespace GW_JPS.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class BillsController : Controller
    {
        private readonly GW_JPSContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public BillsController(GW_JPSContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Bills
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Bill.ToListAsync());
            }
            else if (User.IsInRole("User")){
                return View(await _context.Bill.Where(b => b.CustomerId.Contains(userManager.FindByNameAsync(User.Identity.Name).Result.Id)).ToListAsync());
            }
            return NotFound();
        }

        // GET: Bills/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new BillViewModel
            {
                CustomerId = user.Id,
                PremisesNumber = user.PremisesNumber,
            };

            return View(model);
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("PremisesNumber,CustomerId,Address,Amount")] BillViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genDate = DateTime.Now;

                var bill = new Bill
                {
                    GenerationDate = genDate,
                    DueDate = genDate.AddDays(10),
                    PremisesNumber = model.PremisesNumber,
                    CustomerId = model.CustomerId,
                    Address = model.Address,
                    Amount = model.Amount
                };

                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserBills", new { Id = model.CustomerId });
            }
            return View(model);
        }

        // GET: Bills/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenerationDate,DueDate,PremisesNumber,CustomerId,Address,Amount")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("UserBills", new { Id = bill.CustomerId });
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }*/

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserBills", new { Id = bill.CustomerId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserBills(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with Id = {id} cannot be found";
                return View("NotFound");
            }

            var bills = await _context.Bill.Where(b => b.CustomerId.Contains(id)).ToListAsync();

            var model = new UserBillsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PremisesNumber = user.PremisesNumber,
                Bills = bills
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
