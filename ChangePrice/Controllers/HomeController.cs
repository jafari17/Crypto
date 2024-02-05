using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChangePrice.Services;
using ChangePrice.Data.Repository;
using ChangePrice.Data.Dto;
using ChangePrice.Data.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Web;
using ChangePrice.ModelDataBase;
namespace ChangePrice.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAlertRepository _alertRepository;
        //private IUserRepository _userRepository;
        private IPriceTracking _priceTracking;
        private IAlertAutoRepository _alertAutoRepository;

        private IExchangeProvider _exchangeProvider;
        private IReportUserAlertsDtoRepository _reportUserAlertsDtoRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public HomeController(ILogger<HomeController> logger, IAlertRepository alertRepository, IPriceTracking priceTracking,
                              IExchangeProvider exchangeProvider, IReportUserAlertsDtoRepository reportUserAlertsDtoRepository, UserManager<IdentityUser> userManager,IAlertAutoRepository alertAutoRepository)
        {
            _logger = logger;
            _alertRepository = alertRepository;
            _priceTracking = priceTracking;
            _exchangeProvider = exchangeProvider;
            //_userRepository = userRepository;
            _reportUserAlertsDtoRepository = reportUserAlertsDtoRepository;
            _userManager = userManager;
            _alertAutoRepository = alertAutoRepository;



        }

        public IActionResult Index()
        {
            //_priceTracking.TrackPriceListChanges();


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var listReportUserAlerts = _reportUserAlertsDtoRepository.GetReportUserAlertsByUserId(userId);
            var listReportUserAlertsDesc = listReportUserAlerts.OrderByDescending(l => l.DateRegisterTime);
            ViewBag.LastPrice = _exchangeProvider.GetLastPriceAndSymbol();

            if(_alertAutoRepository.GetAllAlertAuto().Any(AA => AA.UserId == userId))
            {
                ViewBag.UserPriceSteps = _alertAutoRepository.GetAllAlertAuto().First(AA => AA.UserId == userId).PriceSteps;
            }
            else
            {
                ViewBag.UserPriceSteps = 0;
            }
            

            //ViewBag.UserList = _userRepository.GetAllUser();
            return View(listReportUserAlertsDesc);
        }

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
        public IActionResult AddAlertajax(AlertDto alertDto)
        {


            if (alertDto.Price <= 0) 
            {
                return Json(new { message = "Enter a price greater than zero" });
            }
            if (alertDto.UserId == null)
            {
                return Json(new { message = "Select User" });
            }

            //var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                Alert alertNew = new Alert()
                {
                    DateRegisterTime = DateTime.Now,
                    Price = alertDto.Price,
                    Description = alertDto.Description,
                    LastTouchPrice = DateTime.Now.AddYears(-10),
                    UserId = alertDto.UserId,
                    
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