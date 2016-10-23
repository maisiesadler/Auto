using Microsoft.AspNetCore.Mvc;

namespace AutoService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
