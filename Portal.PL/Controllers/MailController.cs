using Microsoft.AspNetCore.Mvc;
using Portal.BL.Helper;
using Portal.BL.Models;
using System.Net;
using System.Net.Mail;


namespace Portal.PL.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailVM model)
        {
            try
            {
                TempData["Msg"] = MailSender.SendMail(model);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Mail Faild";
            }

            return View(model);

        }


    }
}
