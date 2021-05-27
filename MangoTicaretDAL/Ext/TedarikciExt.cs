using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangoTicaretCore;
using MangoTicaretDAL.Base;
using MangoTicaretDAL;

namespace MangoTicaret.DAL.Ext
{
    public class TedarikciExt : Tedarikci
    {
        int total;

        public dynamic TedarikciGetirWithCombo()
        {
            df.sbSql.Append("select ID,Unvan from Tedarikci");
            return GetData();
        }

        public dynamic GetTedarikciList(ListRequest req, enListQueryType type)
        {
            if (type == enListQueryType.Count)
                df.sbSql.Append("Select Count(ID) from Tedarikci");
            else
                df.sbSql.Append("Select * from Tedarikci ");

            df.sbSql.Append(" where Aktif=1");

            if (!string.IsNullOrWhiteSpace(req.filter.Unvan))
            {
                df.sbSql.Append(" AND Unvan LIKE @MusteriUnvan");
                df.param.Add("MusteriUnvan", "%" + Convert.ToString(req.filter.Unvan) + "%");
            }

            if (req.limit <= 0)
                req.limit = total;

            if (type == enListQueryType.Data)
                df.sbSql.Append(" ORDER BY " + req.sort + " " + req.order + " OFFSET " + req.offset + " ROWS FETCH NEXT " + req.limit + " ROWS ONLY ");

            df.sql = df.sbSql.ToString();

            if (type == enListQueryType.Count)
                return total = ExecuteScalarInt();
            else
                return GetData();
        }

    }
}
