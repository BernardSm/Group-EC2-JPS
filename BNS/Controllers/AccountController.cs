using BNS.Models;
using BNS.Data;
using BNS.Models.Identity;
using BNS.ViewModels;
using BNS.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly BNSContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, BNSContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.userEmail, model.userPassword, model.userRememberMe, false);

                if (result.Succeeded)
                {
                    if (!(string.IsNullOrEmpty(ReturnUrl)) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ViewAccount()
        {
            var user = await userManager.GetUserAsync(User);
            var customer = _context.Customer.FirstOrDefault(m => m.userEmail == user.Email);

            return View(customer);
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var CustTrans = _context.Transaction.Where(m => m.customerEmail == user.Email).FirstOrDefault();
            var CustAcct = _context.Customer.Where(m => m.AccountNumber == CustTrans.AccountNum).FirstOrDefault();
            /*            if (ModelState.IsValid)
                        {
                            try
                            { 

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
                        }*/
            return View(CustAcct);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> EditAccount()
        {
            var user = await userManager.GetUserAsync(User);
            var customer = _context.Customer.FirstOrDefault(m => m.userEmail == user.Email);
            return View(customer);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(CustomerEditViewModel model)
        {
            //var user = await userManager.GetUserAsync(User);

            //            if (model.userEmail != user.Email)
            //          {
            //            return NotFound();
            //      }
            Customer customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetName = model.StreetName,
                City = model.City,
                Parish = model.Parish,
                Country = model.Country,
            };

            var appuser = new AppUser
            {
                userFirstName = model.FirstName,
                userLastName = model.LastName,
                userStreetName = model.StreetName,
                userCity = model.City,
                userParish = model.Parish,
                userCountry = model.Country
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    _context.Update(appuser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View(customer);
        }
    }
}
