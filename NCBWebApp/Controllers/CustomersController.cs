using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCBWebApp.Data;
using NCBWebApp.Models;
using NCBWebApp.ViewModels;

namespace NCBWebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly NCBWebAppContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        NCBPaymentServiceReference.NCBPaymentClient client = new NCBPaymentServiceReference.NCBPaymentClient();

        public CustomersController(NCBWebAppContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Teller")]
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var users = new List<ApplicationUser> { };
            var tempUsers = userManager.Users;
            foreach(var user in tempUsers)
            {
                if(await userManager.IsInRoleAsync(user, "Customer"))
                {
                    users.Add(user);
                }
            }
            return View(users);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await userManager.FindByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // Retrieves the information of a single customer
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerInfo()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        [Authorize(Roles = "Customer, Teller")]
        public async Task<IActionResult> Transactions(string? id)
        {
            ApplicationUser user;
            List<AccountTransaction> transactions;
            if (id == null)
            {
                transactions = _context.Transaction.Where(c => c.CusId.Contains(userManager.FindByNameAsync(User.Identity.Name).Result.Id)).ToList();
                user = await userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                transactions = _context.Transaction.Where(c => c.CusId == id).ToList();
                user = await userManager.FindByIdAsync(id);
            }
            var userTransactions = new CustomerTransactionViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Transactions = transactions
            };
            return View(userTransactions);
        }

        [Authorize(Roles = "Teller")]
        public async Task<IActionResult> Withdraw(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            var customer = new CustomerWithdrawViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                Balance = user.Balance,
                AccountNumber = user.AccountNumber,
                CardNumber = user.CardNumber,
                AccountType = user.AccountType
            };

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [Authorize(Roles = "Teller")]
        [HttpPost]
        public async Task<IActionResult> Withdraw(string id, [Bind("Id,Name,Address,Balance,AccountNumber,CardNumber,AccountType, WithdrawalAmount")] CustomerWithdrawViewModel obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await userManager.FindByIdAsync(id);
                    if (obj.WithdrawalAmount <= data.Balance)
                    {
                        data.Balance -= obj.WithdrawalAmount;
                        AccountTransaction transaction = new AccountTransaction
                        {
                            CusId = data.Id,
                            Amount = obj.WithdrawalAmount,
                            TransactionDate = DateTime.Now,
                            TransactionType = "Withdraw"

                        };
                        _context.Transaction.Add(transaction);
                        int mess = _context.SaveChanges();
                        if (mess >= 1)
                        {
                            var result = await userManager.UpdateAsync(data);
                            if (result.Succeeded)
                            {
                                ViewBag.data = "Withdraw Completed";
                                _context.SaveChanges();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Withdraw Failed");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Insufficient Funds Available. Balance on account is {data.Balance}");
                    }
                    return View(obj);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await userManager.FindByIdAsync(obj.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(obj);
        }

        [Authorize(Roles = "Teller")]
        public async Task<IActionResult> Deposit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            var customer = new CustomerDepositViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address,
                Balance = user.Balance,
                AccountNumber = user.AccountNumber,
                CardNumber = user.CardNumber,
                AccountType = user.AccountType
            };

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [Authorize(Roles = "Teller")]
        [HttpPost]
        public async Task<ActionResult> Deposit(string id, [Bind("Id,Name,Address,Balance,AccountNumber,CardNumber,AccountType, DepositAmount")] CustomerDepositViewModel obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var data = await userManager.FindByIdAsync(id);
                    //var data = await _context.Customer.Where(obj1 => obj1.AccountNumber == obj.AccountNumber).FirstOrDefaultAsync();
                    if (!(obj.DepositAmount <=  0))
                    {
                        data.Balance += obj.DepositAmount;
                        AccountTransaction transaction = new AccountTransaction
                        {
                            CusId = data.Id,
                            Amount = obj.DepositAmount,
                            TransactionDate = DateTime.Now,
                            TransactionType = "Deposit"

                        };
                        _context.Transaction.Add(transaction);
                        int mess = _context.SaveChanges();
                        if (mess >= 1)
                        {
                            var result = await userManager.UpdateAsync(data);
                            if (result.Succeeded)
                            {
                                ViewBag.data = "Deposit Completed";
                                _context.SaveChanges();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Deposit Failed");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can not deposit a value less that or equal to 0.");
                    }
                    return View(obj);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await userManager.FindByIdAsync(obj.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(obj);
        }
        
        /*private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }*/

        
    }
}
