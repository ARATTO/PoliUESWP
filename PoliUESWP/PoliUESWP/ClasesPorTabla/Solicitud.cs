using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliUESWP.ClasesPorTabla
{
    class Solicitud
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int IdSolicitud { get; set; }
        public string Motivo { get; set; }
        public string Fecha { get; set; }
        public int Actividad { get; set; }
        public int Tarifa { get; set; }
    }
}
