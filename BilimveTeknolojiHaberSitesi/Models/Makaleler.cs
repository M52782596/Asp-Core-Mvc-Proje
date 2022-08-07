using System;
using System.Collections.Generic;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class Makaleler
    {
        public Makaleler()
        {
            Yorumlars = new HashSet<Yorumlar>();
        }

        public int MakaleId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public int? KulId { get; set; }
        public byte? Sira { get; set; }
        public DateTime? Eklenmetarihi { get; set; }
        public string Resim { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
        public virtual Kullanicilar Kul { get; set; }
       
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
