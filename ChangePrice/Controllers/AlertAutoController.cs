using ChangePrice.Data.Dto;
using ChangePrice.ModelDataBase;
using ChangePrice.Models;
using ChangePrice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Nodes;

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

      
        //public IActionResult AddAlertAutoAjax(int selectedPriceSteps)
        //{

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        //    _alertAutoServies.AddPriceRandNumbers(userId, selectedPriceSteps);


        //    return Redirect("/");
        //}

        [HttpPost]
        public IActionResult AddAlertAutoAjax(int selectedPriceSteps)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _alertAutoServies.AddPriceRandNumbers(userId, selectedPriceSteps);

            return Json($"Alert Auto {selectedPriceSteps} submit ");
        }



        public IActionResult DeleteAlertAuto()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            _alertAutoServies.RemovePriceRandNumbers(userId);


            return Redirect("/");
        }
    }
}
