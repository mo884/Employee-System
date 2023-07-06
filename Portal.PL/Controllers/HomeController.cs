using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Portal.PL.Languages;

namespace Portal.PL.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {


        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public HomeController(IStringLocalizer<SharedResource> SharedLocalizer)
        {
            sharedLocalizer = SharedLocalizer;
        }

        public IActionResult Index()
        {
            ViewBag.data = sharedLocalizer["DASHBOARD"];
            return View();
        }
    }
}
