using System;
using System.Collections.Generic;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class Sayfalar
    {
        public int SayfaId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
        public int? MenuId { get; set; }
        public DateTime? EklenmeTarihi { get; set; }

        public virtual Menuler Menu { get; set; }
    }
}
