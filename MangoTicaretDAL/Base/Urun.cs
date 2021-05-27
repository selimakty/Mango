using MangoTicaretCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Urun", keyColumn: "ID")]
    public class Urun : GenericRepository<Urun>
    {
        public int ID { get; set; }
        public string UrunAdi { get; set; }
        public float Fiyat { get; set; }
        public string ResimUrl { get; set; }
        public int Stok { get; set; }
        public DateTime ModifedDate { get; set; }
        public string Aciklama { get; set; }
          
    }
}
