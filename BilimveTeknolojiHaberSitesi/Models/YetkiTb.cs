using System;
using System.Collections.Generic;

#nullable disable

namespace BilimveTeknolojiHaberSitesi.Models
{
    public partial class YetkiTb
    {
        public YetkiTb()
        {
            Kullanicilars = new HashSet<Kullanicilar>();
        }

        public int YetkiId { get; set; }
        public string YetkiAdi { get; set; }

        public virtual ICollection<Kullanicilar> Kullanicilars { get; set; }
    }
}
