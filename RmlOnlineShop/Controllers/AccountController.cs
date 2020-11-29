using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RmlOnlineShop.Application.ViewModels.AccountViewModels;
using RmlOnlineShop.Data.Models;

namespace RmlOnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;

        public AccountController(
            SignInManager<User> signInManager
            )
        {
            this.signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var res = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.IsStaySignIn, false);
            if (res.Succeeded)
            {
                if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to login!");
                return View(loginViewModel);
            }
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }



    }
}
