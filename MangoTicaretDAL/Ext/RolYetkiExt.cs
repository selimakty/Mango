using MangoTicaretDAL.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTicaretDAL.Ext
{
    public class RolYetkiExt:RolYetki
    {


        
    }
    public class RolYetkiViewModel
    {
        public int RolID { get; set; }
        public string YetkiAdi { get; set; }
        public bool IsChecked { get; set; }
        public int YetkiID { get; set; }
    }

}
