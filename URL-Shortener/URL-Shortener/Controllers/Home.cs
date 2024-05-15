using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
