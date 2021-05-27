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
    public class IlceExt:Ilce
    {
        public List<Ilce> IlceGetir(long sehirID)
        {
            df.sql = "Select * from Ilce where SehirID=@sehirID";
            df.param.Add("sehirID", sehirID);
            return GetModelListWithSQL();
        }
    }
}
