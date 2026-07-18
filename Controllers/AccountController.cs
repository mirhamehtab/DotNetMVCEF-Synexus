using DotNetMVCEF.Models.ViewModels;  
using Microsoft.AspNetCore.Identity;  
using Microsoft.AspNetCore.Mvc;        

namespace DotNetMVCEF.Controllers
{
    public class AccountController : Controller  
    {
       
        private readonly UserManager<IdentityUser> _userManager;     // user create, password hash karna, DB  find
        private readonly SignInManager<IdentityUser> _signInManager; // login cookie, password check karna 

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();   
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)      // unmatched password
                return View(model);        

            var user = new IdentityUser
            {
                UserName = model.Email, 
                Email = model.Email
            };

            // insert user in db
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)   
            {

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Product");   // Product list page par bhej do
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);   
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;   //save in hidden field
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            // PasswordSignInAsync checks entered password with stored password, locks after 5 attempts
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) //secrity check
                    return Redirect(returnUrl);   

                return RedirectToAction("Index", "Product");  
            }

            if (result.IsLockedOut)   
            {
                ModelState.AddModelError(string.Empty, "Too many failed attempts. Try again in a few minutes.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}