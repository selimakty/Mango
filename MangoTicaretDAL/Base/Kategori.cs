using System;
using System.Collections.Generic;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{

    [TableAttributes(table: "Kategori", keyColumn: "ID")]

    public class Kategori : GenericRepository<Kategori>
    {
        public int ID { get; set; }
        public string KategoriAdi { get; set; }
    }
}
