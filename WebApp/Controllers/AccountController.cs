using BL;
using Domain;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.IServices;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ISendGridEmail _sendGridEmail;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IOrderService _orderService;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ISendGridEmail sendGridEmail,
            IOrderService orderService)
        {
            _userManager = userManager;
            _sendGridEmail = sendGridEmail;
            _signInManager = signInManager;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Login() 
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Login);
                var result = await _signInManager.PasswordSignInAsync(user==null? model.Login : user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("List","Shop");
                }
                if (result.RequiresTwoFactor)
                {
                    ModelState.AddModelError("Password", "Two factor required");
                    return View("Login");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("Password", "User account is not allowed .");
                    return View("Login");
                }
                if (result.IsLockedOut)
                { 
                    ModelState.AddModelError("Password", "User account locked out.");
                    return View("Login");
                }
                else
                {
                    ModelState.AddModelError("Password", "Wrong login or pass");
                    return View("Login");
                }
            }
            else
            {
                
                return View("Login");
            }
           
            
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.UserName.Contains(" "))
                {
                    ModelState.AddModelError("UserName", "UserName can not contain spaces");
                    return View("Register");
                }

                AppUser nameUser = await _userManager.FindByNameAsync(model.UserName);
                AppUser emailUser = await _userManager.FindByEmailAsync(model.Email);
                if (nameUser == null)
                {
                    if (emailUser != null)
                    {
                        ModelState.AddModelError("Email", "Email has been taken");
                        return View("Register");
                    }
                    AppUser newUser = new AppUser();
                    newUser.Email = model.Email;
                    newUser.UserName = model.UserName;
                    newUser.EmailConfirmed = true;
                    

                    IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
                    if (result.Succeeded)
                    {
                         result = await _userManager.AddToRoleAsync(newUser, "Member");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", result.Errors.First().Description);
                    }
                    

                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("UserName", "UserName has been taken");
                    return View("Register");
                }
            }

            return View("Register");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("List", "Shop");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnurl = null)
        {
            returnurl = returnurl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //get the info about the user from external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnurl);
                    }
                }
                ModelState.AddModelError("Email", "Error occuresd");
            }
            ViewData["ReturnUrl"] = returnurl;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return RedirectToAction("List", "Shop");
            }
            else
            {
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email });

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnurl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPassword");

                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },protocol:HttpContext.Request.Scheme);

                await _sendGridEmail.SendEmailAsync(model.Email,"Reset pass","<a href=\""+callbackurl+"\"> Click me</a>");
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string code=null)
        {
            return code==null?View("Error"):View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }else
                {
                    ModelState.AddModelError(string.Empty, "Error");
                }

            }
            return View(model);
        }


        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user!= null)
            {
                await _userManager.DeleteAsync(user);
               
            }
            return RedirectToAction("UserList", "Roles");
        }






    }
}
