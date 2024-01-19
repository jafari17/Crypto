using ChangePrice.Data.Dto;
using ChangePrice.Data.Repository;
using ChangePrice.DataBase;
using ChangePrice.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.Controllers
{
    public class UserController : Controller
    {
        IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public IActionResult RemoveUser(int id)
        {
            _userRepository.DeleteUserById(id);
            _userRepository.Save();
            return Redirect("/");
        }
    }
}
