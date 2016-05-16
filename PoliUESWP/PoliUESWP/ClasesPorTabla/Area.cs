using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliUESWP.ClasesPorTabla
{
    class Area
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int IdArea { get; set; }

        public int MaxPersonas { get; set; }

        public string NombreArea { get; set; }
        public string DescArea { get; set; }
    }
}
