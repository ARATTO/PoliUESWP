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
                return String.Empty;
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
        //Metodo Borrar
        public string[] Delete(string dbPath, int idSolicitud)
        {
            if (idSolicitud > 0 || idSolicitud.ToString() != String.Empty)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Solicitud>("SELECT * FROM Solicitud").Where(c => c.IdSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdSolicitud.ToString(), "La Solicitud se elimino correctamente" };

                        db.RunInTransaction(() =>
                        {
                            db.Delete(existing);
                        });

                        return vec;
                    }
                    else {
                        string[] vec = new string[] { idSolicitud.ToString(), "La Solicitud no existe" };
                        return vec;
                    }
                }
            }
            else {
                MessageBox.Show("No a digitado el idSolicitud a borrar");
                string[] vec = new string[] { "", "" };
                return vec;
            }
        }

        ///////////////////////////////////////////////////////
        //Metodo Consulta
        
        public string[] Consulta(string dbPath, int idSolicitud)
        {
            if (!(idSolicitud.ToString() == String.Empty) & idSolicitud > 0 )
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Solicitud>("SELECT * FROM Solicitud").Where(c => c.IdSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] {
                            existing.IdSolicitud.ToString(),
                            existing.Motivo,
                            existing.Fecha,
                            existing.Actividad.ToString(),
                            existing.Tarifa.ToString(),
                            "Encontrado"
                        };
                        return vec;
                    }
                    else
                    {
                        string[] vec = new string[] { "", "", "","","", "La Solicitud no existe" };
                        return vec;
                    }
                }
            }
            else
            {
                MessageBox.Show("ERROR debe digitar el Id de la Solicitud");
                string[] vec = new string[] { "", "", "", "" ,"","" };
                return vec;
            }
        }
        
        ///////////////////////////////////////////////////////
        //Metodo Consulta Ultimo
        public int ConsultaUltimo(string dbPath)
        {
            
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Solicitud>("SELECT * FROM Solicitud").Last();

                    if (existing != null)
                    {
                        int id = existing.IdSolicitud;
                        return id;
                    }
                    else
                    {
                        return -1;
                    }
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
                    if (actividad.ToString() == String.Empty)
                    {
                        MessageBox.Show("ERROR no hay Actividad ingresada ");
                        return true;
                    }
                    else {
                        if (tarifa.ToString() == String.Empty)
                        {
                            MessageBox.Show("ERROR no hay Tarifa ingresada");
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
