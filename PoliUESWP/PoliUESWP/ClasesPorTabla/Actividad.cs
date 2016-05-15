using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliUESWP.ClasesPorTabla
{
    class Actividad
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int IdActividad { get; set; }

        public String NombreActividad { get; set; }
        public String DescripcionActividad { get; set; }
    }
}
