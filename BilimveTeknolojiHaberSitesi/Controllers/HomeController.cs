using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BilimselBilgiler(int id)
        {
            BilimHaberDBContext db = new BilimHaberDBContext();
            var icerik = db.Sayfalars.Where(a=>a.SayfaId==id).FirstOrDefault();
            return View(icerik);
        }
        public IActionResult iletisim()
        {
            //BilimHaberDBContext db = new BilimHaberDBContext();
            //var icerik = db.Sayfalars.Where(a => a.MenuId==3).FirstOrDefault();
            return View();
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
    }
}
