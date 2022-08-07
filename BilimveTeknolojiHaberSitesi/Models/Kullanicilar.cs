using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class Kullanicilar
    {
        public Kullanicilar()
        {
            Yorumlars = new HashSet<Yorumlar>();
            Makalelers = new HashSet<Makaleler>();


        }

       
        public int KullaniciId { get; set; }

        [Display(Name = "Ad")]
        [MinLength(2, ErrorMessage = "En az iki karakter Girilmelidir")]
        [MaxLength(100, ErrorMessage = "En fazla 100 karakter girilebilir")]
        public string Adı { get; set; }

        [Display(Name = "Soyad")]
        [MinLength(2, ErrorMessage = "En az iki karakter Girilmelidir")]
        [MaxLength(100, ErrorMessage = "En fazla 100 karakter girilebilir")]
        public string Soyadı { get; set; }


        [Display(Name = "E posta")]
        [EmailAddress(ErrorMessage = "Lütfen Geçerli Bir Eposta Giriniz")]
        [MaxLength(100, ErrorMessage = "En fazla 100 karakter girilebilir")]
        public string Eposta { get; set; }
        public DateTime? EklenmeTarihi { get; set; }


        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen Parolayı  giriniz")]
        public string Parola { get; set; }
        //[Required(ErrorMessage = "Lütfen Parolayı  giriniz")]
        //[Display(Name = "Parola(Tekrar)")]
        //[Compare("Parola", ErrorMessage = "Parolalar uyuşmuyor")]
      
        //public string parola2 { get; set; }
        public int? YetkiId { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }

        public virtual YetkiTb Yetki { get; set; }
        public virtual ICollection<Makaleler> Makalelers { get; set; }
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
        
    }
}
