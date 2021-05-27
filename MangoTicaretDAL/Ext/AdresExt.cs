using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangoTicaretCore;
using MangoTicaretDAL.Base;
using MangoTicaretDAL;


namespace MangoTicaretDAL.Ext
{
    public class AdresExt : Adres
    {
        public List<dynamic> GetMusteriAdres()
        {
            df.sbSql.Append("Select a.ID,a.AcikAdres,a.AdresAciklama,SehirAdi,IlceAdi from Adres a");
            df.sbSql.Append(" inner join Sehir s on s.ID = a.SehirID");
            df.sbSql.Append(" inner join Ilce i on i.ID = a.IlceID");
            df.sbSql.Append(" Where MusteriID = @MusteriID and a.Aktif = 1");
            df.param.Add("MusteriID", this.MusteriID);

            return GetData();
        }

        
    }
    public class AdresVm
    {
        public int ID { get; set; }
        public string SehirAdi { get; set; }
        public string IlceAdi { get; set; }
        public string AcikAdres { get; set; }
        public string AdresAciklama { get; set; }
    }
}
