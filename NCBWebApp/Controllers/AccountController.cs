using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NCBWebApp.Data;
using NCBWebApp.Models;
using NCBWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCBWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly NCBWebAppContext _context;

        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, NCBWebAppContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Address = model.Address,
                    Balance = model.Balance,
                    AccountNumber = model.AccountNumber,
                    CardNumber = model.CardNumber,
                    AccountType = model.AccountType
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    user = UserManager.FindByNameAsync(user.UserName).Result;
                    _context.Transaction.Add(new AccountTransaction
                    {
                        Amount = model.Balance,
                        TransactionDate = DateTime.Now,
                        TransactionType = "Deposit",
                        CusId = user.Id
                    });
                    int mess = _context.SaveChanges();
                    if (mess >= 1)
                    {
                        if (model.TypeUser == "Admin")
                        {
                            result = await UserManager.AddToRoleAsync(user, "Admin");
                        }
                        else if (model.TypeUser == "Teller")
                        {
                            result = await UserManager.AddToRoleAsync(user, "Teller");
                        }
                        else
                        {
                            result = await UserManager.AddToRoleAsync(user, "Customer");
                        }

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "Cannot add selected roles to user");
                            return View(model);
                        }

                        if (SignInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Administration");
                        }

                        await SignInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registration Failed");
                    }

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} already exists");
            }
        }

        //NEEDS TO BE CHECKED
        /*[AcceptVerbs("Get", "Post")]
        public IActionResult IsPremisesNumberInUse(int number)
        {
            var pn = _context.Users.FirstOrDefault(m => m.PremisesNumber == number);

            if (pn == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Premises number {number} already exists");
            }
        }*/

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
