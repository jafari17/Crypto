using ChangePrice.Data;
using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult RemovePrice(decimal price)
        {
            ReadWriteContext _RWContext = new ReadWriteContext();
            string txt = _RWContext.ReadFile();

            try
            {
                List<RegisterPrice> registerPriceList = JsonConvert.DeserializeObject<List<RegisterPrice>>(txt);

                List<RegisterPrice> registerPriceListNew = JsonConvert.DeserializeObject<List<RegisterPrice>>("[]");

                foreach (var rp in registerPriceList)
                {
                    if (rp.price != price)
                    {
                        registerPriceListNew.Add(rp);

                    }
                }

                string jsonString = JsonSerializer.Serialize(registerPriceListNew);
                _RWContext.WriteFile(jsonString);

                return Redirect("/");
            }
            catch
            {
                Console.WriteLine("Json Convert Error");
            }



            //var orderDetail = _context.OrderDetails.Find(id);
            //_context.Remove(orderDetail);
            //_context.SaveChanges();

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