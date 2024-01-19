
using Microsoft.AspNetCore.Mvc;

namespace ChangePrice.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            
            return View("Index");
        }
    }
}
