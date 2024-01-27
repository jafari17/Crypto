
using ChangePrice.Data.DataBase;
using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.Notification;
using ChangePrice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        private readonly ILogger _logger;
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ILogger<NotificationEmail> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            
            if (!ModelState.IsValid)
                return View(model);


            var UserName = _userManager.Users.ToList().Find(a => a.UserName == model.UserName);

            if (UserName != null)
            {
                ModelState.AddModelError("Email", "نام کاربری تکرای است");
                return View(model);
            }



            var UserEmail = _userManager.Users.ToList().Find(a => a.Email == model.Email);

            if (UserEmail != null)
            {
                ModelState.AddModelError("Email", "ایمیل تکرای است");
                return View(model);
            }

            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                EmailConfirmed = true
            }, model.Password);

            ViewBag.Result = false;

            if (result.Succeeded)
            {
                ViewBag.Result = true;
                return Redirect("/");
            }


            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
                return View(model);
            }

            var result =
                await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("LoginWith2fa");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "حساب کابری شما قفل شده است");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "تلاش برای ورود نامعتبر میباشد");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
