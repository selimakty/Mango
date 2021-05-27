using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Ilce", keyColumn: "ID")]
    public class Ilce : GenericRepository<Ilce>
    {
        public int ID { get; set; }
        public int SehirID { get; set; }
        public string IlceAdi { get; set; }
    }
}
