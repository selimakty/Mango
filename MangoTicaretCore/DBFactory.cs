using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using Dapper;
using Newtonsoft;
using System.Data;
using System.Data.SqlClient;

namespace MangoTicaretCore
{
    public class DBConfig
    {
        public String Server = "";
        public String DBName = "";
        public String UName = "";
        public String UPwd = "";
        private readonly string _connectionString = string.Empty;

        public DBConfig()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;


            Server = root.GetSection("ConnectionStrings").GetSection("MasterDBServer").Value;
            DBName = root.GetSection("ConnectionStrings").GetSection("MasterDBName").Value;
            UName = root.GetSection("ConnectionStrings").GetSection("MasterDBUserName").Value;
            UPwd = root.GetSection("ConnectionStrings").GetSection("MasterDBUserPwd").Value;
        }

        public string ConnectionString
        {
            get => _connectionString;
        }
    }


    public class DBFactory : IDisposable
    {
        public string sql = "";
        public StringBuilder sbSql = new StringBuilder();

        public IDbConnection con = null;
        public DynamicParameters param = new DynamicParameters();
        public int timeOut = 6000;
        public string conStr;

        public DBFactory()
        {
            DBConfig db = new DBConfig();

            conStr = db.ConnectionString;
        }

        public DBFactory(DBConfig db)
        {
            if (db == null) return;
            timeOut = 120;

            conStr = "data source=" + db.Server + ";database=" + db.DBName + ";uid=" + db.UName + ";pwd=" + db.UPwd + ";language=turkish; pooling=true;";
        }

        public void Connect()
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    return;
                }
                else
                    return;
            }

            con = new SqlConnection(conStr);
            con.Open();
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
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();

                    con.Dispose();
                }
            }
        }

        #endregion
    }

    public class TableAttributes : Attribute
    {
        public TableAttributes(string table, string keyColumn)
        {
            this.Table = table;
            this.KeyColumn = keyColumn;
        }

        public string Table { get; set; }
        public string KeyColumn { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NoUpdate : Attribute
    {
        public NoUpdate()
        {
        }
    }
}
