using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BNS.Data;
using BNS.Models;
using BNS.ViewModels.Customer;
using BNS.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BNS.Controllers
{
    public class TellersController : Controller
    {
        private readonly BNSContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public TellersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, BNSContext context)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Tellers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teller.ToListAsync());
        }

        // GET: Tellers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teller = await _context.Teller
                .FirstOrDefaultAsync(m => m.TellerId == id);
            if (teller == null)
            {
                return NotFound();
            }

            return View(teller);
        }

        // GET: Tellers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            Teller teller = new Teller
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                userEmail = model.userEmail,
                userPassword = model.userPassword,
                userConfirmedPassword = model.userConfirmedPassword
            };

            var user = new AppUser
            {
                UserName = model.userEmail,
                Email = model.userEmail,
                userFirstName = model.FirstName,
                userLastName = model.LastName
            };

            var result = await userManager.CreateAsync(user, model.userPassword);

            if (result.Succeeded)
            {
                //Add user role to user
                await userManager.AddToRoleAsync(user, "Teller");
                _context.Add(teller);
                await _context.SaveChangesAsync();

                if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                //await signInManager.SignInAsync(user, isPersistent: false);
                //return RedirectToAction("index", "home");
                return RedirectToAction("ListRoles", "Admin");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(teller);
        }

        // GET: Tellers/Edit/5
        [Authorize(Roles = "Admin")]
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TellerId,FirstName,LastName,userEmail,userPassword,userConfirmedPassword")] Teller teller)
        {
            if (id != teller.TellerId)
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
                    if (!TellerExists(teller.TellerId))
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teller = await _context.Teller
                .FirstOrDefaultAsync(m => m.TellerId == id);
            if (teller == null)
            {
                return NotFound();
            }

            return View(teller);
        }

        // POST: Tellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teller = await _context.Teller.FindAsync(id);
            _context.Teller.Remove(teller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TellerExists(int id)
        {
            return _context.Teller.Any(e => e.TellerId == id);
        }
    }
}
