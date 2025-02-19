using Microsoft.AspNetCore.Mvc;

namespace ApiDms.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
