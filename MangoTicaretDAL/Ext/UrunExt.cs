using MangoTicaret;
using MangoTicaretCore;
using MangoTicaretDAL.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Ext
{
    public class UrunExt:Urun
    {   int total = 0;
        public dynamic GetUrunList(ListRequest req, enListQueryType type)
        {
            if (type == enListQueryType.Count)
                df.sbSql.Append("Select Count(u.ID) from Urun u");
            else
                df.sbSql.Append("Select * from Urun u ");

            df.sbSql.Append(" where u.Aktif=1");

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

        public dynamic GetFavoriList(ListRequest req, enListQueryType type,int musteriID)
        {
            if (type == enListQueryType.Count)
                df.sbSql.Append("Select Count(f.ID) from Favoriler f");
            else
                df.sbSql.Append("Select f. ID, UrunID , UrunAdi , Fiyat from Favoriler f ");

            df.sbSql.Append(" inner join Urun u on u.ID = f.UrunID");

            df.sbSql.Append(" where u.Aktif=1 and f.Aktif =1");

            df.sbSql.Append(" and f.MusteriID = @musteriID ");

            df.param.Add("musteriID", musteriID);

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

        public dynamic GetSepetList(ListRequest req, enListQueryType type, int musteriID)
        {
            if (type == enListQueryType.Count)
                df.sbSql.Append("Select Count(f.ID) from Sepet f");
            else
                df.sbSql.Append("Select f.ID,UrunID,UrunAdi,Fiyat,Adet,sum(adet*Fiyat) as ToplamFiyat from Sepet f ");

            df.sbSql.Append(" inner join Urun u on u.ID = f.UrunID");

            df.sbSql.Append(" where u.Aktif=1 and f.Aktif =1");

            df.sbSql.Append(" and f.MusteriID = @musteriID ");

            df.param.Add("musteriID", musteriID);

            //if (!string.IsNullOrWhiteSpace(req.filter.Unvan))
            //{
            //    df.sbSql.Append(" AND Unvan LIKE @MusteriUnvan");
            //    df.param.Add("MusteriUnvan", "%" + Convert.ToString(req.filter.Unvan) + "%");
            //}

            if (req.limit <= 0)
                req.limit = total;

            if (type == enListQueryType.Data)
                df.sbSql.Append(" group by f.ID,UrunID,UrunAdi,Fiyat,Adet ");

            df.sql = df.sbSql.ToString();

            if (type == enListQueryType.Count)
                return total = ExecuteScalarInt();
            else
                return GetData();
        }

        public int FavoriKontrol(int musteriId,int urunid) 
        {
            df.sbSql.Append("select * from Favoriler where MusteriID = @musteriID and UrunID = @urunID");

            df.sbSql.Append(" and Aktif = 1");

            df.param.Add("musteriID",musteriId);
            df.param.Add("urunID",urunid);

            df.sql = df.sbSql.ToString();
            return ExecuteScalarInt();
        }

        public int SepetKontrol(int musteriId, int urunid)
        {
            df.sbSql.Append("select ID from Sepet where MusteriID = @musteriID and UrunID = @urunID");

            df.sbSql.Append(" and Aktif = 1");

            df.param.Add("musteriID", musteriId);
            df.param.Add("urunID", urunid);

            df.sql = df.sbSql.ToString();
            return ExecuteScalarInt();
        }
    }
}
