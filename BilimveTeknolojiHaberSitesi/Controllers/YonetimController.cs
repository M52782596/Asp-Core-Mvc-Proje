using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    [Authorize]
    public class YonetimController : Controller
    {
        BilimHaberDBContext db = new BilimHaberDBContext();

       
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Sayfalar()
        {
            var sayfalar = db.Sayfalars.Include(m=>m.Menu).Where(a => a.Silindi == false).OrderBy(b => b.EklenmeTarihi).ToList();

            return View(sayfalar);
        }
       
        public IActionResult SayfaEkle()
        {

            var Menuler=(from m in db.Menulers.Where(m=>m.Silindi==false&&m.Aktif==true).ToList()
            select new SelectListItem
            {
                Text=m.Baslik,
                Value=m.MenuId.ToString()
            }
            );

            ViewBag.MenuId = Menuler;
           
            return View();
        }
      
        [HttpPost]
      
        public IActionResult SayfaEkle(Sayfalar s)
        {
            s.Silindi = false;
            s.EklenmeTarihi = DateTime.Now;
            db.Sayfalars.Add(s);
            db.SaveChanges();

            return RedirectToAction("Sayfalar");
        }
  
        public IActionResult SayfaGetir(int id)
        {
            var menuler= (from m in db.Menulers.Where(m => m.Silindi == false ).ToList()
                         select new SelectListItem
                         {
                             Text = m.Baslik,
                             Value = m.MenuId.ToString()
                         }
           ) ;
            

            var sayfa = db.Sayfalars.Where(m => m.Silindi == false && m.SayfaId == id).FirstOrDefault();

            
            ViewBag.MenuId = menuler;

            return View("SayfaGuncelle", sayfa);

        }
    
        public IActionResult SayfaGuncelle(Sayfalar sayfa)
        {
            var guncellenek = db.Sayfalars.Where(s=>s.Silindi==false&&s.SayfaId==sayfa.SayfaId).FirstOrDefault();
            guncellenek.Baslik = sayfa.Baslik;
            guncellenek.Icerik = sayfa.Icerik;
            guncellenek.Aktif = sayfa.Aktif;
            db.Sayfalars.Update(guncellenek);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");
        }
     
        public IActionResult SayfaSil(int id)
        {
            var silinecek = db.Sayfalars.Where(s => s.Silindi == false && s.SayfaId == id).FirstOrDefault();

            silinecek.Silindi =true;
            db.Sayfalars.Update(silinecek);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");

        }
        
       public IActionResult TumMakaleler()

        {
            
            var Makaleler = db.Makalelers.Include(a=>a.Kul).Where(m => m.Silindi == false).OrderByDescending(a => a.Eklenmetarihi).ToList();

            return View(Makaleler);
        }
        public IActionResult MakaleİcerikGetir(int id)
        {
            var makaleler = db.Makalelers.Where(m => m.MakaleId == id).FirstOrDefault();
            return View("MakaleOnayla", makaleler);
        }
        public IActionResult MakaleOnayla( Makaleler m)

        {

            var guncellenek = db.Makalelers.Where(s => s.Silindi == false && s.MakaleId == m.MakaleId).FirstOrDefault();
            guncellenek.Aktif = m.Aktif;

            db.Makalelers.Update(guncellenek);
            db.SaveChanges();
            return RedirectToAction("TumMakaleler");
        }
        public IActionResult Makaleler()

        {
            int kullid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Makaleler = db.Makalelers.Where(m => m.Silindi == false && m.KulId == kullid).OrderByDescending(a=>a.Eklenmetarihi).ToList();

            return View(Makaleler);
        }
        
        public IActionResult MakaleEkle()
        {
           
            return View();
        }
       
        [HttpPost]
   
        public IActionResult MakaleEkle(Makaleler m)
        {
            int kullid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            m.Silindi = false;
            m.Aktif = false;
            m.KulId = kullid;
            m.Eklenmetarihi = DateTime.Now;
            db.Makalelers.Add(m);
            db.SaveChanges();
            return RedirectToAction("Makaleler");
        }
      
        public IActionResult MakaleGetir(int id)
        {
            var makaleler = db.Makalelers.Where(m => m.MakaleId == id).FirstOrDefault();
                return View("MakaleGuncelle",makaleler);
        }
      
        public IActionResult MakaleGuncelle(Makaleler m)
        {
            var guncellenek = db.Makalelers.Where(s => s.Silindi == false && s.MakaleId == m.MakaleId).FirstOrDefault();
            guncellenek.Baslik = m.Baslik;
            guncellenek.Icerik = m.Icerik;
            guncellenek.Resim = m.Icerik;
          
            db.Makalelers.Update(guncellenek);
            db.SaveChanges();
            return RedirectToAction("Makaleler");
        }
    
        public IActionResult MakaleSil(int id)
        {
            var silinecek = db.Makalelers.Where(s => s.Silindi == false && s.MakaleId == id).FirstOrDefault();

            silinecek.Silindi = true;
            db.Makalelers.Update(silinecek);
            db.SaveChanges();
            return RedirectToAction("Makaleler");

        }

      
        public IActionResult MakaleYorumlari(int id)

        {
            var yorumlar = db.Yorumlars.Include(a => a.Makale).Include(b=>b.Kul).Where(s => s.Silindi == false && s.MakaleId == id).FirstOrDefault();

            return RedirectToAction("Yorumlar",yorumlar);
        }
        [HttpGet]
        public IActionResult Yorumlar()

        {
            var yorumlar = db.Yorumlars.Include(a => a.Makale).Include(b => b.Kul).Where(s => s.Silindi == false).ToList();
           

            return View(yorumlar);
        }


        [HttpPost]
        public IActionResult Yorumlar(string listelemeturu)

        {
            var yorumlar = db.Yorumlars.Include(a=>a.Makale).Include(b => b.Kul).Where(s => s.Silindi == false).ToList();
            switch (listelemeturu)
            {
                case "Onayli":
                    yorumlar = db.Yorumlars.Include(a => a.Makale).Include(b => b.Kul).Where(s => s.Silindi == false && s.Aktif == true).ToList();
                    break;
                case "Onaysiz":
                    yorumlar = db.Yorumlars.Include(a => a.Makale).Include(b => b.Kul).Where(s => s.Silindi == false && s.Aktif == false).ToList();
                    break;

            }

            return View(yorumlar);
        }
        public IActionResult Onayla(int id)
        {
            var yorum= db.Yorumlars.Where(s => s.Silindi == false&&s.YorumId==id).FirstOrDefault();
            yorum.Aktif = Convert.ToBoolean(-1 * Convert.ToInt32(yorum.Aktif) + 1);
            db.Yorumlars.Update(yorum);
            db.SaveChanges();
            return RedirectToAction("Yorumlar");
        }
        public IActionResult YorumSil(int id)
        {
            var yorum = db.Yorumlars.Where(s => s.Silindi == false && s.YorumId == id).FirstOrDefault();
            yorum.Silindi = true;
            db.Yorumlars.Update(yorum);
            db.SaveChanges();
            return RedirectToAction("Yorumlar");
        }

        public IActionResult Kullanicilar()
        {
            var kul = db.Kullanicilars.Where(a => a.Silindi == false).ToList();


            return View("Kullanicilar",kul);
        }
       public  IActionResult KullaniciEkle()
        {
            var Yetkiler = (from m in db.YetkiTbs.ToList()
                            select new SelectListItem
                            {
                                Text = m.YetkiAdi,
                                Value = m.YetkiId.ToString()
                            }
            );
            ViewBag.YetkiId = Yetkiler;

            return View();
        }
        [HttpPost]
        public IActionResult KullaniciEkle(Kullanicilar m)
        {

            m.Silindi = false;
           
           
            m.EklenmeTarihi = DateTime.Now;
            db.Kullanicilars.Add(m);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");
        }

        public IActionResult KullaniciGetir(int id)
        {
            var Yetki = (from m in db.YetkiTbs.ToList()
                           select new SelectListItem
                           {
                               Text = m.YetkiAdi,
                               Value = m.YetkiId.ToString()
                           }
           );

            var kullanici= db.Kullanicilars.Where(a => a.Silindi == false&&a.KullaniciId==id).ToList();



            ViewBag.YetkiId = Yetki;

            return View("KullaniciGuncelle", kullanici);

        }

        public IActionResult KullaniciGuncelle(Kullanicilar k)
        {
            var kullanici = db.Kullanicilars.Where(a => a.Silindi == false && a.KullaniciId == k.KullaniciId).FirstOrDefault();
            kullanici.Adı = k.Adı;
            kullanici.Soyadı = k.Soyadı;
            kullanici.Eposta = k.Eposta;
          
            try
            {
                if (k.Parola.Trim().Length != 0)
                {
                    kullanici.Parola = MD5Sifrele(k.Parola);
                }


            }
            catch
            { }
            kullanici.Aktif = k.Aktif;
            db.Kullanicilars.Update(kullanici);
            db.SaveChanges();
            return RedirectToAction("Sayfalar");
        }

        public IActionResult KullaniciSil(int id)
        {
            var silinecek = db.Kullanicilars.Where(s => s.Silindi == false && s.KullaniciId == id).FirstOrDefault();

            silinecek.Silindi = true;
            db.Kullanicilars.Update(silinecek);
            db.SaveChanges();
            return RedirectToAction("Kullanicilar");

        }


        //[HttpPost]
        //public IActionResult Kullanicilar(YetkiTb yetki,string onay)
        //{
        //    var Kullanicilar = db.Kullanicilars.Where(a => a.Silindi == false).OrderBy(y => y.YetkiId).ToList();

        //    switch (onay)
        //    {
        //        case "Onayli": Kullanicilar = db.Kullanicilars.Where(a => a.Silindi == false&&a.YetkiId==yetki.YetkiId&&a.Aktif==true).OrderBy(y => y.YetkiId).ToList();
        //            break;
        //        case "Onaysiz":
        //            Kullanicilar = db.Kullanicilars.Where(a => a.Silindi == false && a.YetkiId == yetki.YetkiId && a.Aktif == false).OrderBy(y => y.YetkiId).ToList();
        //            break;

        //    }
        //    return View(Kullanicilar);
        //}


        public IActionResult Bilgilerim()
        {
            int kullid = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var kullanici = db.Kullanicilars.Find(kullid);
            kullanici.Parola = "";

            return View(kullanici);
        }
        public IActionResult BilgilerimGuncelle(Kullanicilar k)
        {
            var kullanici = db.Kullanicilars.Where(a => a.Silindi == false && a.KullaniciId == k.KullaniciId).FirstOrDefault();
            kullanici.Adı = k.Adı;
            kullanici.Soyadı = k.Soyadı;
            kullanici.Eposta = k.Eposta;
            try
            {
                if (k.Parola.Trim().Length != 0)
                {
                    kullanici.Parola = MD5Sifrele(k.Parola);
                }

            }
            catch 
            { }
            db.Kullanicilars.Update(kullanici);
            db.SaveChanges();

            return RedirectToAction("Bilgilerim");
        }
        
        public IActionResult CikisYap()
        {
            return View();
        }
        [Authorize(Roles = "Ad")]
        public IActionResult Menuler()
        {
            var menuler = db.Menulers.Where(a => a.Silindi == false).OrderBy(b => b.Sira).ToList();

            return View(menuler);
        }
        [Authorize(Roles = "Ad")]
        public IActionResult MenuEkle()
        {
           var menuler = (from m in db.Menulers.Where(m => m.Silindi == false&&m.UstId==null&&m.Aktif==true).ToList()
                           select new SelectListItem
                           {
                               Text = m.Baslik,
                               Value = m.MenuId.ToString()
                           }
           );

           var bos = new List<SelectListItem>() { 
                new SelectListItem()
            {
              
                Text = "Yok",
                Value = "null"
            
           }};
            
           
            ViewBag.ustmenu = menuler.Union(bos).OrderBy(s=>s.Text);

            

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Ad")]
        public IActionResult MenuEkle(Menuler s)
        {

            s.Silindi = false;
            db.Menulers.Add(s);
            db.SaveChanges();

            return RedirectToAction("Menuler");
        }
        [Authorize(Roles = "Ad")]
        public IActionResult MenuGetir(int id)
        {
            var menu = db.Menulers.Where(m => m.Silindi == false && m.MenuId == id).FirstOrDefault();
            var menuler = (from m in db.Menulers.Where(m => m.Silindi == false && m.UstId == null && m.Aktif == true).ToList()
                           select new SelectListItem
                           {
                               Text = m.Baslik,
                               Value = m.MenuId.ToString()
                           }
    );

            var bos = new List<SelectListItem>() {
                new SelectListItem()
            {

                Text = "Yok",
                Value = "null"

           }};


            ViewBag.ustmenu = menuler.Union(bos).OrderBy(s => s.Text);

            return View("MenuGuncelle", menu);

        }
        [Authorize(Roles = "Ad")]
        public IActionResult MenuGuncelle(Menuler menu)
        {
            var guncellenek = db.Menulers.Where(s => s.Silindi == false && s.MenuId == menu.MenuId).FirstOrDefault();
            guncellenek.Baslik = menu.Baslik;
            guncellenek.Url = menu.Url;
            guncellenek.Aktif = menu.Aktif;
            db.Menulers.Update(guncellenek);
            db.SaveChanges();
            return RedirectToAction("Menuler");
        }
        [Authorize(Roles = "Ad")]
        public IActionResult MenuSil(int id)
        {
            var silinecek = db.Menulers.Where(s => s.Silindi == false && s.MenuId == id).FirstOrDefault();

            silinecek.Silindi = true;
            db.Menulers.Update(silinecek);
            db.SaveChanges();
            return RedirectToAction("Menuler");

        }
        [Authorize(Roles = "Ad")]
        public IActionResult MenuSayfalari(int id)
        {
            var sayfalar = db.Sayfalars.Where(a => a.Silindi == false && a.MenuId == id).ToList();
            return View("Sayfalar",sayfalar);
        }
        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = System.Text.Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            System.Text.StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }
    }
}
