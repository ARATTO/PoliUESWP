using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliUESWP.ClasesPorTabla
{
    class Tarifa
    {
        [SQLite.Unique, SQLite.AutoIncrement]
        public int IdTarifa { get; set; }

        public int CantidadPersonas { get; set; }
        public double TarifaUnica { get; set; }
    }
}
