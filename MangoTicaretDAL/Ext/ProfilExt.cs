using MangoTicaretDAL.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Ext
{
    public class ProfilExt:Profil
    {

        public int CheckLogin(string kullaniciadi, string pass)
        {
            df.sql = "select ID from Profil where KullaniciAdi = @KullaniciAdi and Sifre=@Password";
            df.param.Add("KullaniciAdi", kullaniciadi);
            df.param.Add("Password", pass);
            return ExecuteScalarInt();
        }

        public int GetPermission(int RolID, int YetkiID)
        {
            df.sql = "select ID from RolYetki where RolID = @RolID and YetkiID =@YetkiID";
            df.param.Add("YetkiID", YetkiID);
            df.param.Add("RolID", RolID);
            return ExecuteScalarInt();
        }

        public bool CheckUserName ()
        {
            df.sql = "select ID from Profil where KullaniciAdi = @KullaniciAdi";
            df.param.Add("KullaniciAdi", this.KullaniciAdi);

            int count = ExecuteScalarInt();

            if (count > 0)
                return false; 
            else
                return true;
        }

    }

    public class RegisterVm 
    {
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Re_Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class ProfilVm
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
    }
}
