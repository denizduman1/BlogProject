using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
