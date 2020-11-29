using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.ViewModels;
using EFDbFirstApproachExample.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using EFDbFirstApproachExample.Filters;

namespace EFDbFirstApproachExample.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Registers
        [ActionName("Register")]
        public ActionResult RegistrationPage()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                //register
                var appDbContext = new ApplicationDBContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var passwordHash = Crypto.HashPassword(rvm.Password);
                var user = new ApplicationUser() { Email = rvm.Email, UserName = rvm.Username, PasswordHash = passwordHash, City = rvm.City, Country = rvm.Country, Birthday = rvm.DateOfBirth, Address = rvm.Address, PhoneNumber = rvm.Mobile };
                IdentityResult result = userManager.Create(user);

                if (result.Succeeded)
                {
                    //role
                    userManager.AddToRole(user.Id, "Customer");

                    //login
                    this.LoginUser(userManager, user);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid data");
                return View();
            }
        }
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [OverrideExceptionFilters]
        public ActionResult Login(LoginViewModel lvm)
        {
            
                //login
                var appDbContext = new ApplicationDBContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var user = userManager.Find(lvm.Username, lvm.Password);
                if (user != null)
                {
                //login
                this.LoginUser(userManager, user);

                    if (userManager.IsInRole(user.Id,"Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    if (userManager.IsInRole(user.Id, "Manager"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Manager" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("myerror", "Invalid Username or Password");
                    return View();
                }           
        }

        [NonAction]
        public void LoginUser(ApplicationUserManager userManager, ApplicationUser user)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            var appDBContext = new ApplicationDBContext();
            var userStore = new ApplicationUserStore(appDBContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser appUser = userManager.FindById(User.Identity.GetUserId());
            return View(appUser);
        }
    }
}