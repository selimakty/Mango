using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Adres", keyColumn: "ID")]
    public class Adres : GenericRepository<Adres>
    {
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public int SehirID { get; set; }
        public int IlceID { get; set; }
        public string AcikAdres { get; set; }
        public string AdresAciklama { get; set; }
    }
}
