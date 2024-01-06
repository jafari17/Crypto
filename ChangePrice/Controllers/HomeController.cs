using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChangePrice.Repository;
using ChangePrice.Services;

namespace ChangePrice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPriceRepository _priceRepository;
        private IPriceTracking _priceTracking;
        public HomeController(ILogger<HomeController> logger, IPriceRepository priceRepository, IPriceTracking priceTracking )
        {
            _logger = logger;
            _priceRepository = priceRepository;
            _priceTracking = priceTracking;
        }

        public IActionResult Index()
        {
            _priceTracking.TrackPriceListChanges();

            List<RegisterPriceModel> AllPrice = _priceRepository.GetList();

            return View(AllPrice);
        }

        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RegisterPriceModel registerPriceModel)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(registerPriceModel);
            //}

            RegisterPriceModel rpNew = new RegisterPriceModel()
            {
                Id = Guid.NewGuid(),
                DateRegisterTime = DateTime.Now,
                price = registerPriceModel.price,
                Description = registerPriceModel.Description,
                LastTouchPrice = registerPriceModel.LastTouchPrice,
            };


            List<RegisterPriceModel> AllPrice = _priceRepository.GetList();
            AllPrice.Add(rpNew);
            _priceRepository.Add(AllPrice);

            return Redirect("/");
        }

        public IActionResult RemovePrice(Guid id)
        {
            try
            {
                List<RegisterPriceModel> AllPrice = _priceRepository.GetList();

                var item = AllPrice.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    AllPrice.Remove(item);
                }


                _priceRepository.Add(AllPrice);

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