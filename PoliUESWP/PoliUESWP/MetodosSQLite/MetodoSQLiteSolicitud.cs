using PoliUESWP.ClasesPorTabla;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PoliUESWP.MetodosSQLite
{
    class MetodoSQLiteSolicitud
    {
        ///////////////////////////////////////////////////////
        //INSERTAR ACTIVIDAD

        public string Insert(string dbPath, string motivo, string fecha, int actividad, int tarifa)
        {
            if (vacios(motivo, fecha, actividad, tarifa) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Solicitud()
                        {
                            Motivo = motivo,
                            Fecha = fecha,
                            Actividad = actividad,
                            Tarifa = tarifa
                        });
                    });

                    return "Se guardo con Exito";

                }
            }
            else {
                return "";
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Update

        public string Update(string dbPath, int idSolicitud, string motivo, string fecha, int actividad, int tarifa)
        {
            if (vacios(motivo, fecha, actividad, tarifa) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    var existing = db.Query<Solicitud>("SELECT * FROM Solicitud").Where(c => c.IdSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Motivo = motivo;
                        existing.Fecha = fecha;
                        existing.Actividad = actividad;
                        existing.Tarifa = tarifa;

                        db.RunInTransaction(() =>
                        {
                            db.Update(existing);
                        });
                        return "La Solicitud con el ID : " + existing.IdSolicitud + " se actualiza con exito"; ;
                    }
                    return "La Solicitud con ese ID no existe";
                }
            }
            else {
                return "Debe llenar todos los campos";
            }
        }
        ///////////////////////////////////////////////////////
        //Campos Vacios

        public bool vacios(string motivo, string fecha, int actividad, int tarifa)
        {
            if (motivo == String.Empty)
            {
                MessageBox.Show("ERROR ingrese un motivo para la Solicitud");
                return true;
            }
            else {
                if (fecha == String.Empty)
                {
                    MessageBox.Show("ERROR fecha ingresada incorrecta");
                    return true;
                }
                else
                {
                    if (actividad <= 0)
                    {
                        MessageBox.Show("ERROR la Actividad ingresada es incorrecta");
                        return true;
                    }
                    else {
                        if (tarifa <= 0)
                        {
                            MessageBox.Show("ERROR tarifa ingresada incorrecta");
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    
                }
            }

        }
        ///////////////////////////////////////////////////////
    }
}
