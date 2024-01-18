using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }


    }
}
