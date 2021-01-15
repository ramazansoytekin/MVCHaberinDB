using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHaberinDB.Models
{
    public class Yorum
    {
        public int YorumID { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string  YorumMetni { get; set; }
        public int? HaberID { get; set; }

    }
}