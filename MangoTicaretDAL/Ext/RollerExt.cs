using MangoTicaret;
using MangoTicaretCore;
using MangoTicaretDAL.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Ext
{
    public class RollerExt:Roller
    {
        public dynamic GetRolList(ListRequest req, enListQueryType type)
        {
            StringBuilder sb = new StringBuilder();

            if (type == enListQueryType.Count)
                sb.Append(" SELECT count(Y.ID) from Roller Y  Where Y.Aktif = 1");
            else
            {
                sb.Append(" Select Y.ID,Y.RolAdi from Roller Y ");

                sb.Append(" WHERE Y.Aktif = 1 ");


                // request.limit  : kaç kayıt istiyorsun
                // request.offset : kaçıncı kayıttan başlayacağı
            }
            if (type == enListQueryType.Data)
                sb.Append(" ORDER BY " + req.sort + " " + req.order + " OFFSET " + req.offset + " ROWS FETCH NEXT " + req.limit + " ROWS ONLY ");

            df.sql = sb.ToString();

            if (type == enListQueryType.Count)
                return ExecuteScalarInt();
            else
                return GetData();
        }
    }
}
