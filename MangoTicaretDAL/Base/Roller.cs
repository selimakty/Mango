using System;
using System.Collections.Generic;
using System.Text;
using MangoTicaretCore;

namespace MangoTicaretDAL.Base
{
    [TableAttributes(table: "Roller", keyColumn: "ID")]
    public class Roller : GenericRepository<Roller>
    {
        public int ID { get; set; }
        public string RolAdi { get; set; }
    }
}
