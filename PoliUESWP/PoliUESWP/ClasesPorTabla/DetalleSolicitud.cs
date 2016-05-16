using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliUESWP.ClasesPorTabla
{
    class DetalleSolicitud
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int IdDetalleSolicitud { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public double CobroTotal { get; set; }
        public int IDSolicitud { get; set; }
        public int IDArea { get; set; }
    }
}
