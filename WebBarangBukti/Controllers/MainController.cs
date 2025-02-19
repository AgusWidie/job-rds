using Microsoft.AspNetCore.Mvc;

namespace WebBarangBukti.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
