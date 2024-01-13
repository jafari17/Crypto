using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChangePrice.Repository;
using ChangePrice.Services;
using System.Net.Mail;

namespace ChangePrice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPriceRepository _priceRepository;
        private IPriceTracking _priceTracking;
        private IExchangeProvider _exchangeProvider;

        public HomeController(ILogger<HomeController> logger, IPriceRepository priceRepository, IPriceTracking priceTracking, IExchangeProvider exchangeProvider)
        {
            _logger = logger;
            _priceRepository = priceRepository;
            _priceTracking = priceTracking;
            _exchangeProvider = exchangeProvider;
            
        }

        public IActionResult Index()
        {
            //_priceTracking.TrackPriceListChanges();

            var listAlert = _priceRepository.GetList();

            ViewBag.LastPrice = _exchangeProvider.GetLastPrice();

            return View(listAlert);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AlertModel alertModel)
        {

            //decimal price;
            //if (decimal.TryParse(alertModel.price)
            //{
            //    return Redirect("/");
            //}


            AlertModel alertNew = new AlertModel()
            {
                Id = Guid.NewGuid(),
                DateRegisterTime = DateTime.Now,
                price = alertModel.price,
                EmailAddress = alertModel.EmailAddress,
                Description = alertModel.Description,
                LastTouchPrice = alertModel.LastTouchPrice,
            };


            List<AlertModel> listAlert = _priceRepository.GetList();
            listAlert.Add(alertNew);
            _priceRepository.Add(listAlert);

            return Redirect("/");
        }

        public IActionResult RemovePrice(Guid id)
        {
            try
            {
                List<AlertModel> listAlert = _priceRepository.GetList();

                var item = listAlert.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    listAlert.Remove(item);
                }


                _priceRepository.Add(listAlert);

                return Redirect("/");
            }
            catch
            {
                Console.WriteLine("Remove Price Error");
            }

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