using System;
using System.Collections.Generic;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{

    [TableAttributes(table: "Tedarikci", keyColumn: "ID")]

    public partial class Tedarikci : GenericRepository<Tedarikci>
    {
        public long ID { get; set; }
        public string Unvan { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNo { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string CepTelefon { get; set; }
        public string Kontak { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string BankaAd { get; set; }
        public string SubeAd { get; set; }
        public string SubeKodu { get; set; }
        public string IbanNo { get; set; }
        public string HesapNo { get; set; }
    }
}
