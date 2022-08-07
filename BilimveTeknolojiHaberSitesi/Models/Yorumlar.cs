using System;
using System.Collections.Generic;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class Yorumlar
    {
        public int YorumId { get; set; }
        public string Yorum { get; set; }
        public int? KulId { get; set; }
        public int? MakaleId { get; set; }
        public DateTime? Tarih { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }

        public virtual Kullanicilar Kul { get; set; }
        public virtual Makaleler Makale { get; set; }
    }
}
