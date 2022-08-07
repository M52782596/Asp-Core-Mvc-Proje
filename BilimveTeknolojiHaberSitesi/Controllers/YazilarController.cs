using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    public class YazilarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult MakaleDetayi(int id)
        {

            BilimHaberDBContext db = new BilimHaberDBContext();
            var icerik = db.Makalelers.Where(a => a.MakaleId == id).FirstOrDefault();
            TempData["mesaj"] = "";
            return View(icerik);

           
        }
       
       
        public IActionResult YorumYap(Yorumlar y)
        {
            BilimHaberDBContext db = new BilimHaberDBContext();
            y.Silindi = false;
            y.Aktif = false;
            y.Tarih = DateTime.Now;
            db.Yorumlars.Add(y);
            db.SaveChanges();
            TempData["mesaj"] = "Yorumunuz Başarıyla Alındı Yönetici Onayından Sonra Görebilirsiniz";
            return Redirect("MakaleDetayi/" + y.MakaleId);

        }
    }
}
