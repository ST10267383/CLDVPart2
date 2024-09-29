using Microsoft.AspNetCore.Mvc;

namespace WebAppCLDV6212Part2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
