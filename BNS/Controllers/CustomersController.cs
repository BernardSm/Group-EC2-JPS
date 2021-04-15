using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BNS.Data;
using BNS.Models;
using BNS.Models.Identity;
using Microsoft.AspNetCore.Identity;
using BNS.ViewModels;
using BNS.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;

namespace BNS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly BNSContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public CustomersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, BNSContext context)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Customers 
        //ViewAllCustomer
        [Authorize(Roles = "Teller,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        [Authorize(Roles = "Teller")]
        public async Task<IActionResult> GetAccount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        //[Authorize(Roles = "Teller")]
        public async Task<IActionResult> CustomerTransaction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trans = await _context.Transaction.FirstOrDefaultAsync(m => m.customerID == id);

            if (trans == null)
            {
                return NotFound();
            }

            return View(trans);
        }

        public async Task<IActionResult> AllTransaction()
        {
            return View(await _context.Transaction.ToListAsync());
        }


        public async Task<IActionResult> Deposit(int? id)
        {
            
            return View();
        }

        public async Task<IActionResult> Withdraw(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Customer
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }









        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetName = model.StreetName,
                    City = model.City,
                    Parish = model.Parish,
                    Country = model.Country,
                    userEmail = model.userEmail,
                    userPassword = model.userPassword,
                    userConfirmedPassword = model.userConfirmedPassword,
                    AccountNumber = model.AccountNumber,
                    CardNumber = model.CardNumber,
                    AccountType = model.AccountType,
                    Balance = model.Balance
                };

                var user = new AppUser
                {
                    UserName = model.userEmail,
                    Email = model.userEmail,
                    userFirstName = model.FirstName,
                    userLastName = model.LastName,
                    userStreetName = model.StreetName,
                    userCity = model.City,
                    userParish = model.Parish,
                    userCountry = model.Country
                };

                var result = await userManager.CreateAsync(user, model.userPassword);

                if (result.Succeeded)
                {
                    //Add user role to user
                    await userManager.AddToRoleAsync(user, "Customer");
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index", "home");
                    return RedirectToAction("Index", "Customers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> usedEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email is not available");
            }
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> usedAccountNumber(long AccountNum)
        {
            var Account = await _context.Customer
            .FirstOrDefaultAsync(m => m.AccountNumber == AccountNum);
            if (Account == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Account Number is not available, Generate again");
            }
        }
        /*[HttpPost]
        public IActionResult GenAccount(string GenAcc)
        {
            if (GenAcc == "Generate")
            {
                Random ranAcc = new Random();
                long genAcc = ranAcc.Next(1000000, 9999999);
                ViewBag.ResultCardNum = genAcc;
            }
            return View();
        }

        [HttpPost]
        public IActionResult GenCard(string GenCrd)
        {
            Random ranCard = new Random();
            long genCard = ranCard.Next(100000000, 199999999);

            string get = "400" + genCard;
            long here = Convert.ToInt64(get);
            ViewBag.ResultAccNum = here;//genAccount;
            return View();
        }
        */

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> usedCardNumber(long CardNum)
        {
            var Card = await _context.Customer
            .FirstOrDefaultAsync(m => m.CardNumber == CardNum);

            if (Card == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Card Number is not available, Generate again");
            }
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAccount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(int id, [Bind("UserId,FirstName,LastName,StreetName,City,Parish,Country,userEmail,userPassword,userConfirmedPassword,AccountNumber,CardNumber,AccountType,Balance")] Customer customer)
        {
            if (id != customer.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    var appuser = new AppUser
                    {
                        userFirstName = customer.FirstName,
                        userLastName = customer.LastName,
                        userStreetName = customer.StreetName,
                        userCity = customer.City,
                        userParish = customer.Parish,
                        userCountry = customer.Country,
                        Email = customer.userEmail
                    };
                    var cust = await _context.Customer.FindAsync(id);

                    //var user = await userManager.FindByEmailAsync(customer.userEmail);
                    if (cust.userEmail == appuser.Email)
                    {
                        _context.Update(appuser);

                        var Use = cust.userEmail.Where(r => r.Equals(customer.userEmail)).ToList()
                            .ForEach(c => { c.userCountry = customer.Country});

                            //Rows.SelectMany(r => r.Columns)

                       //.Where(c => c.Email.ToLower().Equals(customer.userEmail));
                        foreach (var col in Use)
                        {
                            //col.IsUpdated = true;
                            col
                        }

                    }*/

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.UserId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.UserId == id);
        }
    }
}
