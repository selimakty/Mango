using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MangoTicaretCore
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class, new()
    {
        public DBFactory df;

        public GenericRepository()
        {

        }

        public long Save()
        {
            if (df == null)
            {
                throw new Exception("DBFactory null olamaz");
            }

            Type myType = this.GetType();
            var tableAttrib = myType.GetCustomAttributes(true).Where(a => a.GetType() == typeof(TableAttributes)).Select(a =>
            {
                return a as TableAttributes;
            }).FirstOrDefault();
             
            string sFields = "";

            PropertyInfo[] myPropertyInfo = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo IDPropInfo = myType.GetProperty(tableAttrib.KeyColumn);
             
            bool isInsert = false;
            if (Convert.ToInt64(IDPropInfo.GetValue(this, null)) <= 0)
                isInsert = true;

            foreach (PropertyInfo p in myPropertyInfo)
            {
                if (isInsert)
                {
                    if (p.Name != tableAttrib.KeyColumn)
                    {
                        sFields += "@" + p.Name + ",";
                        df.param.Add(p.Name, p.GetValue(this, null));
                    }
                }
                else
                {
                    object[] noUpdateAttributes = p.GetCustomAttributes(typeof(NoUpdate), false);

                    if (p.Name != tableAttrib.KeyColumn && noUpdateAttributes.Length <= 0)
                        sFields += p.Name + "=@" + p.Name + ",";

                    df.param.Add(p.Name, p.GetValue(this, null));
                }
            }

            sFields = sFields.Substring(0, sFields.Length - 1);

            if (isInsert)
                df.sql = "insert into " + tableAttrib.Table + " (" + sFields.Replace("@", "") + ") values (" + sFields + ") select scope_identity() as id ";
            else
                df.sql = "update " + tableAttrib.Table + " set " + sFields + " where " + tableAttrib.KeyColumn + "=@" + tableAttrib.KeyColumn + ";  select @" + tableAttrib.KeyColumn;

            df.Connect();
            long id = Convert.ToInt64(df.con.ExecuteScalar(df.sql, df.param, null, df.timeOut));
            df.param = new DynamicParameters();
            return id;
        }

        public void Delete(long id)
        {
            Type myType = this.GetType();
            var tableAttrib = myType.GetCustomAttributes(true).Where(a => a.GetType() == typeof(TableAttributes)).Select(a =>
            {
                return a as TableAttributes;
            }).FirstOrDefault();

            df.sql = "delete from " + tableAttrib.Table + " where " + tableAttrib.KeyColumn + "=@" + tableAttrib.KeyColumn;
            df.param.Add(tableAttrib.KeyColumn, id);
            df.Connect();
            Convert.ToInt64(df.con.ExecuteScalar(df.sql, df.param, null, df.timeOut));
            df.param = new DynamicParameters();
        }

        public void Pasif()
        {
            Type myType = this.GetType();
            var tableAttrib = myType.GetCustomAttributes(true).Where(a => a.GetType() == typeof(TableAttributes)).Select(a =>
            {
                return a as TableAttributes;
            }).FirstOrDefault();

            PropertyInfo IDPropInfo = myType.GetProperty(tableAttrib.KeyColumn);
            long id = Convert.ToInt64(IDPropInfo.GetValue(this, null));

            if (id <= 0)
            {
                throw new Exception("ID 0 veya 0'dan küçük olamaz");
            }

            df.sql = "update s set Aktif=0 from " + tableAttrib.Table + " s where " + tableAttrib.KeyColumn + "=@" + tableAttrib.KeyColumn;
            df.param.Add(tableAttrib.KeyColumn, id);
            df.Connect();

            df.con.Execute(df.sql, df.param, null, df.timeOut);

            df.param = new DynamicParameters();
        }

        public List<dynamic> GetData()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }                

            df.Connect();
            List<dynamic> lst = df.con.Query<dynamic>(df.sql, df.param, null, true, df.timeOut).ToList<dynamic>();
            df.param = new DynamicParameters();
            return lst;
        }

        /// <summary>
        /// Verilen modelin tablosunu list olarak döndürür, SQL cümlesi gerekmez
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetModelList()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            Type myType = this.GetType();
            var tableAttrib = myType.GetCustomAttributes(true).Where(a => a.GetType() == typeof(TableAttributes)).Select(a =>
            {
                return a as TableAttributes;
            }).FirstOrDefault();

            df.sql = "SELECT * FROM " + tableAttrib.Table;

            return GetModelListWithSQL();
        }

        /// <summary>
        /// Verilen SQL e ait modeli list olarak döndürür
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetModelListWithSQL()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            df.Connect();

            List<TEntity> lst = df.con.Query<TEntity>(df.sql, df.param, null, true, df.timeOut).ToList<TEntity>();
            df.param = new DynamicParameters();
            return lst;
        }

        /// <summary>
        /// SQL yazmadan sadece ilgili modeli döndürür, Default ID (KeyColumn) yi baz alır
        /// </summary>
        /// <returns>TEntity</returns>
        public TEntity GetModel()  // ID'yi girdiğimizde elde ettiğimiz modeli döndürür
        {
            Type myType = this.GetType();
            var tableAttrib = myType.GetCustomAttributes(true).Where(a => a.GetType() == typeof(TableAttributes)).Select(a =>
            {
                return a as TableAttributes;
            }).FirstOrDefault();

            df.sql = "SELECT * FROM " + tableAttrib.Table + " WHERE " + tableAttrib.KeyColumn + "=@id";

            df.param.Add("id", myType.GetProperty(tableAttrib.KeyColumn).GetValue(this, null));

            return GetModelWithSQL();
        }

        /// <summary>
        /// Verilen SQL e ait modeli döndürür
        /// </summary>
        /// <returns></returns>
        public TEntity GetModelWithSQL()  // Srogudan Geri Dönen Modeli döndürür
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            df.Connect();

            List<TEntity> lst = df.con.Query<TEntity>(df.sql, df.param, null, true, df.timeOut).ToList<TEntity>();
            df.param = new DynamicParameters();

            if (lst.Count > 0)
                return lst[0];
            else
                return new TEntity();
        }

        public void Execute()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            df.Connect();
            df.con.Execute(df.sql, df.param, null, df.timeOut);
            df.param = new DynamicParameters();
        }

        public object ExecuteScalar()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            df.Connect();
            var o = df.con.ExecuteScalar(df.sql, df.param, null, df.timeOut);
            df.param = new DynamicParameters();
            return o;
        }

        public double ExecuteScalarDouble()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            object val = ExecuteScalar();
            return val == null ? 0 : tools.ToDouble(val);
        }

        public long ExecuteScalarLong()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            object val = ExecuteScalar();
            return val == null ? 0 : tools.ToLong(val);
        }

        public int ExecuteScalarInt()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            object val = ExecuteScalar();
            return val == null ? 0 : tools.ToInt(val);
        }

        public string ExecuteScalarString()
        {
            if (df.sbSql.Length > 0)
            {
                df.sql = df.sbSql.ToString();
                df.sbSql.Clear();
            }

            object val = ExecuteScalar();
            return val == null ? "" : val.ToString();
        }

        #region Dispose

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected internal void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                if (this.df != null)
                {
                    this.df.Dispose();
                }
            }
        }

        #endregion
    }
}
