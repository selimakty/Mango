using MangoTicaretCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Profil", keyColumn: "ID")]
    public class Profil : GenericRepository<Profil>
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public string ResimUrl { get; set; }
        public int RolID { get; set; }
    }
}
