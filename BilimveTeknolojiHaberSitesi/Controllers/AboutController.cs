using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    public class AboutController : Controller
    {
        BilimHaberDBContext db = new BilimHaberDBContext();
        public IActionResult index(int id)
        {
           
            var sayfa = db.Sayfalars.Where(a => a.Aktif == true && a.Silindi == false && a.MenuId == id).FirstOrDefault();
            return View(sayfa);
        }
        [HttpGet]
        public IActionResult Yazarlik_Basvuru()
        {
            

            return View();
        }
        [HttpPost]
        public IActionResult Yazarlik_Basvuru(Kullanicilar k)
        {
            k.YetkiId = 2;
            k.Aktif = false;
            k.Silindi = false;
            if (k.Parola.Length> 0)
            {
              k.Parola = MD5Sifrele(k.Parola);
            }
            else
            {

            }
            
            k.EklenmeTarihi = DateTime.Now;
             db.Kullanicilars.Add(k);
             db.SaveChanges();


            TempData["Yazarmesaji"] = "Başvurunuz Başarıyla Alındı. Yönetici Onayından sonra bilgilendirileceksiniz";



            return View();
        }

        [HttpGet]
        public IActionResult UyeKayitFormu()
        {


            return View();
        }
        [HttpPost]
        public IActionResult UyeKayitFormu(Kullanicilar k)
        {
           
            if (k.Parola.Length >0)
            {
                k.YetkiId = 3;
                k.Aktif = false;
                k.Silindi = false;
                k.Parola = MD5Sifrele(k.Parola);
                k.EklenmeTarihi = DateTime.Now;
                db.Kullanicilars.Add(k);
                db.SaveChanges();
                TempData["Uyemesaji"] = "Başvurunuz Başarıyla Alındı. Yönetici Onayından sonra bilgilendirileceksiniz";
            }
            else
            {

            }

           
           

            return View();
        }

        static string MD5Sifrele(string sifrelenecekMetin)
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
