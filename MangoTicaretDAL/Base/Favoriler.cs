using MangoTicaretCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Favoriler", keyColumn: "ID")]
    public class Favoriler : GenericRepository<Favoriler>
    {
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public int UrunID { get; set; }
    }
}
