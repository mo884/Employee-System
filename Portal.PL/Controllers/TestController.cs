using Microsoft.AspNetCore.Mvc;

namespace Portal.PL.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
