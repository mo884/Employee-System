using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.BL.Models;
using Portal.DAL.Extend;
using System.Diagnostics;

namespace Portal.PL.Controllers
{
    public class AccountController : Controller
    {



        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }




        #region Registration

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }


        #endregion


        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            var userName = await userManager.FindByNameAsync(model.UserName);
            var userEmail = await userManager.FindByEmailAsync(model.UserName);

            dynamic result;

            if (userEmail != null)
            {
                 result = await signInManager.PasswordSignInAsync(userEmail, model.Password, model.RememberMe, false);
            }
            else
            {
                 result = await signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, false);
            }

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");

            }

            return View(model);
        }

        #endregion


        #region Sign Out

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion


        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                //MailSender.Mail("Password Reset", passwordResetLink);
                //logger.Log(LogLevel.Warning, passwordResetLink);

                EventLog log = new EventLog();
                log.Source = "Inventory System";
                log.WriteEntry(passwordResetLink, EventLogEntryType.Information);

                return RedirectToAction("ConfirmForgetPassword");
            }

            return RedirectToAction("ConfirmForgetPassword");
        }

        #endregion



        #region Reset Password

        public IActionResult ResetPassword(string? Email , string? Token)
        {
            if(Email != null && Token != null)
            {
                return View();
            }

            return RedirectToAction("ForgetPassword");
        }

        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmResetPassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            return RedirectToAction("ConfirmResetPassword");
        }



        #endregion


    }
}
