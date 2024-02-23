using HamburgerMVC4.Data;
using HamburgerMVC4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace HamburgerMVC4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index(int? cid)
        {
            IQueryable<Menu> menus = _db.Menus;

            if (cid != null)
            {
                menus = menus.Where(x => x.CategoryId == cid);

                ViewBag.Category = _db.Categories.Find(cid)!.Name;
            }

            return View(await menus.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ContactUs(MailViewModel vm)
        {
            if (vm.Subject != null && vm.Body != null)
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();


                vm.Sender = "hpakelgil@gmail.com";
                vm.Reciever = "hpakelgil@gmail.com";

                message.From = new MailAddress(vm.Sender);
                message.To.Add(new MailAddress(vm.Reciever));
                message.Subject = vm.Subject;
                message.Body = vm.Body;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new NetworkCredential(vm.Sender, "qrloxdzwgliorcsc");

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtpClient.Send(message);

                return RedirectToAction("ContactUsConfirmed");
            }
            return View();
        }

        public IActionResult ContactUsConfirmed(MailViewModel vm)
        {
            return View();
        }






    }
}

