using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Yetkiler", keyColumn: "ID")]
    public class Yetkiler : GenericRepository<Yetkiler>
    {
        public int ID { get; set; }
        public string YetkiAdi { get; set; }
    }
}
