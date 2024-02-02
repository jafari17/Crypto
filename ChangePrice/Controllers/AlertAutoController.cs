using ChangePrice.Data.Dto;
using ChangePrice.Model_DataBase;
using ChangePrice.Models;
using ChangePrice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChangePrice.Controllers
{
    public class AlertAutoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAlertAutoServies _alertAutoServies;

        public AlertAutoController(IAlertAutoServies alertAutoServies, ILogger<HomeController> logger)
        {
            _alertAutoServies = alertAutoServies;
            _logger = logger;
            

        }

        //public IActionResult AddAlertAuto()
        //{
        //    return View();
        //}

        //[HttpPost]
        public IActionResult AddAlertAuto()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            _alertAutoServies.AddPriceRandNumbers(userId);


            return Redirect("/");
        }
        public IActionResult DeleteAlertAuto()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            _alertAutoServies.RemovePriceRandNumbers(userId);


            return Redirect("/");
        }
    }
}
