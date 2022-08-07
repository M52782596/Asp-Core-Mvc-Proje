using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    public class HaberlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         
          public IActionResult HaberDetayi(int id)
        {
            BilimHaberDBContext db = new BilimHaberDBContext();
            var icerik = db.Sayfalars.Where(a => a.SayfaId == id).FirstOrDefault();
            return View(icerik);
         
        }
    }
}
