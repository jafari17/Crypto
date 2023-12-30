using ChangePrice.Data;
using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace ChangePrice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private ReadWriteContext _RWContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_RWContext = RWContext;
        }

        public IActionResult Index()
        {
            ReadWriteContext _RWContext = new ReadWriteContext();
            string txt = _RWContext.ReadFile();

            try
            {
                List<RegisterPrice> registerPriceList = JsonConvert.DeserializeObject<List<RegisterPrice>>(txt);
                return View(registerPriceList);
            }
            catch
            {
                Console.WriteLine("Json Convert Error");
            }

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RegisterPrice registerPrice)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(registerPrice);
            //}

            RegisterPrice rp = new RegisterPrice()
            {
                DateRegisterTime = DateTime.Now,
                price = registerPrice.price,
                Description = registerPrice.Description,
                LastTouchPrice = registerPrice.LastTouchPrice,
            };
            ReadWriteContext _RWContext = new ReadWriteContext();

            string txt = _RWContext.ReadFile();
            List<RegisterPrice> registerPriceList = JsonConvert.DeserializeObject<List<RegisterPrice>>(txt);


            registerPriceList.Add(rp);

            string jsonString = JsonSerializer.Serialize(registerPriceList);
            _RWContext.WriteFile(jsonString);

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