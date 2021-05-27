using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Sehir", keyColumn: "ID")]
    public class Sehir : GenericRepository<Sehir>
    {
        public int ID { get; set; }
        public string SehirAdi { get; set; }
    }
}
