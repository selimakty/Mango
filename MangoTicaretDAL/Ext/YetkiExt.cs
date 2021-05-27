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
    public class YetkiExt:Yetkiler
    {
        public List<dynamic> GetYetki(int id)
        {
            df.sql = "select y.ID, y.YetkiAdi,isnull(RolID,-1) as RoleID from Yetkiler y left join RolYetki r on y.ID =r.YetkiID and RolID=@RolId and r.Aktif=1";
            df.param.Add("RolId", id);
            return GetData();
        }

        public int DeleteYetki(int id)
        {
            df.sql = "UPDATE RolYetki SET Aktif = 0 where RolID=@RolID";
            df.param.Add("RolID", id);
            return ExecuteScalarInt();
        }
    }
}
