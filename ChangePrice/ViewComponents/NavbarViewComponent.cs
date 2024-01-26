using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.ViewComponents
{
    [Authorize]
    public class NavbarViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        public NavbarViewComponent(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IViewComponentResult Invoke(string UserId)
        {

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _userManager.FindByIdAsync(UserId).Result;

            ViewBag.UserList = user;
            return View("Index");
        }
    }
}


