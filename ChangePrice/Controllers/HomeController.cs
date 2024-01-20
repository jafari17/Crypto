using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChangePrice.Services;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Net;
using ChangePrice.Data.Repository;
using ChangePrice.DataBase;
using ChangePrice.Data.Dto;

namespace ChangePrice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAlertRepository _alertRepository;
        private IUserRepository _userRepository;
        private IPriceTracking _priceTracking;
        private IExchangeProvider _exchangeProvider;
        private IReportUserAlertsDtoRepository _reportUserAlertsDtoRepository;
        


        public HomeController(ILogger<HomeController> logger, IAlertRepository alertRepository, IPriceTracking priceTracking,
                              IExchangeProvider exchangeProvider, IUserRepository userRepository, IReportUserAlertsDtoRepository reportUserAlertsDtoRepository)
        {
            _logger = logger;
            _alertRepository = alertRepository;
            _priceTracking = priceTracking;
            _exchangeProvider = exchangeProvider;
            _userRepository = userRepository;
            _reportUserAlertsDtoRepository = reportUserAlertsDtoRepository;
        }

        public IActionResult Index()
        {
            //_priceTracking.TrackPriceListChanges();

            var listReportUserAlerts = _reportUserAlertsDtoRepository.GetAllReportUserAlerts();
            var listReportUserAlertsDesc = listReportUserAlerts.OrderByDescending(o => o.DateRegisterTime);
            ViewBag.LastPrice = _exchangeProvider.GetLastPrice();
            ViewBag.UserList = _userRepository.GetAllUser();
            return View(listReportUserAlertsDesc);
        }



        //public IActionResult Index(int id = -1)
        //{
        //    //_priceTracking.TrackPriceListChanges();
        //    ViewBag.LastPrice = _exchangeProvider.GetLastPrice();
        //    ViewBag.UserList = _userRepository.GetAllUser();
        //    if (id == -1)
        //    {
        //        var listReportUserAlerts = _reportUserAlertsDtoRepository.GetAllReportUserAlerts();
        //        return View(listReportUserAlerts);
        //    }
        //    else
        //    {
        //        var listReportUserAlerts = _reportUserAlertsDtoRepository.GetReportUserAlertsByUserId(id);
        //        return View(listReportUserAlerts);
        //    }

        //}





        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Alert alert)
        {

           
            if (alert.Price > 0) { }
            else
            {
                return Redirect("/");
            }
            if (alert.UserId == null)
            {
                return Redirect("/");
            }

            Alert alertNew = new Alert()
            {
                AlertId = alert.AlertId,
                DateRegisterTime = DateTime.Now,
                Price = alert.Price,
                Description = alert.Description,
                LastTouchPrice = DateTime.Now.AddYears(-10),
                UserId = alert.UserId
            };

            _alertRepository.InsertAlert(alertNew);
            _alertRepository.Save();

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult AddAlertajax(int price, int UserId, string Description)
        {


            if (price <= 0) 
            {
                return Json(new { message = "Enter a price greater than zero" });
            }
            if (UserId <= 0)
            {
                return Json(new { message = "Select User" });
            }


            try
            {
                Alert alertNew = new Alert()
                {
                    DateRegisterTime = DateTime.Now,
                    Price = price,
                    Description = Description,
                    LastTouchPrice = DateTime.Now.AddYears(-10),
                    UserId = UserId
                };

                _alertRepository.InsertAlert(alertNew);
                _alertRepository.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("", e.Message);
                _logger.LogError("", e.Message);
                return Json(new { message = "There was a problem saving in the database"});
            }
            return Json(new { message = true });
        }




        public IActionResult RemovePrice(int id)
        {
            _alertRepository.DeleteAlertById(id);
            _alertRepository.Save();
            return Redirect("/");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}