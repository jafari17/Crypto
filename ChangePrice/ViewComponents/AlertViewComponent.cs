
using ChangePrice.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.ViewComponents
{
    public class AlertViewComponent : ViewComponent
    {
        private IUserRepository _userRepository;
        public AlertViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.UserList = _userRepository.GetAllUser();
            return View("Index");
        }
    }
}