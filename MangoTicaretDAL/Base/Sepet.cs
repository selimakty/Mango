using MangoTicaretCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Sepet", keyColumn: "ID")]
    public class Sepet : GenericRepository<Sepet>
    {
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }
    }
}
