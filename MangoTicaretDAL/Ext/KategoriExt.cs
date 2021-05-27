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
    public class KategoriExt : Kategori
    {
        int total;

        public Kategori GetKategori()
        {
            df.sbSql.Append("select * from Kategori");
            df.sbSql.Append(" where Aktif=1");
            return GetModelWithSQL();
        }

        public dynamic GetKategoriList(ListRequest req, enListQueryType type)
        {
            if (type == enListQueryType.Count)
                df.sbSql.Append("Select Count(ID) from Kategori");
            else
                df.sbSql.Append("Select * from Kategori ");

            df.sbSql.Append(" where Aktif=1");

            //if (!string.IsNullOrWhiteSpace(req.filter.Unvan))
            //{
            //    df.sbSql.Append(" AND Unvan LIKE @MusteriUnvan");
            //    df.param.Add("MusteriUnvan", "%" + Convert.ToString(req.filter.Unvan) + "%");
            //}

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
