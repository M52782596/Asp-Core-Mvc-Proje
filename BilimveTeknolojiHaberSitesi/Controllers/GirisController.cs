using BilimveTeknolojiHaberSitesi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BilimveTeknolojiHaberSitesi.Controllers
{
    public class GirisController : Controller
    {
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanicilar k, string ReturnUrl)
        {
            string yetki = "";
            BilimHaberDBContext db = new BilimHaberDBContext();
            var kullanici = db.Kullanicilars.Where(kul => kul.Aktif == true && kul.Silindi == false && kul.Eposta == k.Eposta && kul.Parola == MD5Sifrele(k.Parola)).FirstOrDefault();


            if (kullanici != null)
            {
                switch (kullanici.YetkiId)
                {
                    case 1: yetki = "Ad"; break;
                    case 2: yetki = "Yazar"; break;
                    case 3: yetki = "Üye"; break;
                   
                }
               
              
                var talepler = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,kullanici.Eposta),
                    new Claim(ClaimTypes.Role,yetki.ToString()),
                     new Claim(ClaimTypes.NameIdentifier,kullanici.KullaniciId.ToString())
                };

                ClaimsIdentity kimlik = new ClaimsIdentity(talepler," ");
                ClaimsPrincipal kural = new ClaimsPrincipal(kimlik);
                await HttpContext.SignInAsync(kural);
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {

                  
                    if (kullanici.YetkiId == 1)
                    {
                        return Redirect("/Yonetim/Index");
                    }
                    if (kullanici.YetkiId == 2)
                    {
                        return Redirect("/Yonetim/Makaleler");
                    }
                    if(kullanici.YetkiId == 3)
                    {
                        return Redirect("/Home/Index");
                    }
                }
            }




            return View();

        }
        public async  Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
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


