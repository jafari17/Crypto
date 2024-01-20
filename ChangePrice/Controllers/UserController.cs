using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.DataBase;
using ChangePrice.Models;
using ChangePrice.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace ChangePrice.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(IUserRepository userRepository, ILogger<NotificationEmail> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        public IActionResult Index()
        {
            var listUser = _userRepository.GetAllUser();
            return View(listUser);
        }

        //private IActionResult View(object userPartial)
        //{
        //    var listUser = _userRepository.GetAllUser();
        //    return View(listUser);
        //}

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserName user)
        {




            UserName userNew = new UserName()
            {
                Name = user.Name,
                EmailAddress = user.EmailAddress,
                IsActive = true,
            };

            _userRepository.InsertUser(userNew);
            _userRepository.Save();
            return Redirect("/");
        }


        [HttpPost]
        public IActionResult AddUserajax(string Name, string EmailAddress)
        {

            if (Name == null || Name == "" )
            {
                return Json(new { message = "Name is empty" }); ;
            }
            if (EmailAddress == null || EmailAddress == "")
            {
                return Json(new { message = "EmailAddress is empty" }); ;
            }
            if(_userRepository.GetAllUser().Any(u => u.EmailAddress == EmailAddress))
            {
                return Json(new { message = "Duplicate email" }); ;
            }

            try
            {
                UserName userNew = new UserName()
                {
                    Name = Name,
                    EmailAddress = EmailAddress,
                    IsActive = true,
                };

                _userRepository.InsertUser(userNew);
                _userRepository.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("", e.Message);
                _logger.LogError("", e.Message);
                return Json(new { message = "There was a problem saving in the database" });
            }
            return Json(new { message = "User added successfully!" });
        }


        public IActionResult RemoveUser(int id)
        {
            _userRepository.DeleteUserById(id);
            _userRepository.Save();
            return Redirect("/user/");
        }
    }
}
