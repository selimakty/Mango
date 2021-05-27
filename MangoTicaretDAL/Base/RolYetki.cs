using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "RolYetki", keyColumn: "ID")]
    public class RolYetki : GenericRepository<RolYetki>
    {
        public int ID { get; set; }
        public int RolID { get; set; }
        public int YetkiID { get; set; }
    }
}
