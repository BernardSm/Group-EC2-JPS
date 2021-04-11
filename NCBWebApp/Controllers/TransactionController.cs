using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCBWebApp.Data;
using NCBWebApp.Models;

namespace NCBWebApp.Controllers
{
    //[Authorize(Roles = "Teller")]
    public class TransactionController : Controller
    {
        private readonly NCBWebAppContext _context;

        public TransactionController(NCBWebAppContext context)
        {
            _context = context;
        }
        
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        public async Task<IActionResult> Withdraw(int? id)
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
        public async Task<IActionResult> Withdraw(int id, [Bind("Id,Name,Address,Balance,AccountNumber,CardNumber,AccountType")] Customer obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    var data = await _context.Customer.Where(obj1 => obj1.AccountNumber == obj.AccountNumber).FirstOrDefaultAsync();
                    if (obj.Balance <= data.Balance)
                    {
                        data.Balance -= obj.Balance;
                        AccountTransaction transaction = new AccountTransaction
                        {
                            CusId = obj.Id,
                            Amount = obj.Balance,
                            TransactionDate = DateTime.Now,
                            TransactionType = "Withdraw"

                        };
                        _context.Transaction.Add(transaction);
                        int mess = _context.SaveChanges();
                        if (mess == 1)
                        {
                            ViewBag.data = "Withdraw Completed";
                            _context.SaveChanges();
                        }
                        else
                        {
                            ViewBag.data = "Withdraw Failed";
                        }
                    }
                    else
                    {
                        ViewBag.data = "Insufficient Funds Available";
                    }
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(obj.Id))
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
            return View(obj);
        }

        public async Task<IActionResult> Deposit(int? id)
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
        public ActionResult Deposit(int id, [Bind("Id,Name,Address,Balance,AccountNumber,CardNumber,AccountType")] Customer obj, AccountTransaction account)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var data = _context.Customer.Where(obj1 => obj1.Id == obj.Id).FirstOrDefault();
                    data.Balance += obj.Balance;
                    AccountTransaction transaction = new AccountTransaction 
                    {
                        CusId = obj.Id,
                        Amount = obj.Balance,
                        TransactionDate = DateTime.Now,
                        TransactionType = "Deposit"

                    };
                    _context.Transaction.Add(transaction);
                    int mess = _context.SaveChanges();
                    if (mess == 1)
                    {
                        
                        ViewBag.data = "Deposit Completed";

                        
                    }
                    else
                    {
                        ViewBag.data = "Deposit Failed";
                    }

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(obj.Id))
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
            return View(obj);
        }

        //[Authorize(Roles = "Customer")]
        public IActionResult Display(int id)
        {
            if(id == 0)
            {
                return Redirect("Transaction");
            }

            var cusTransaction = _context.Transaction.Where(cus => cus.CusId == id).ToList();

            
            return View(cusTransaction);
        }


               
        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
   
}