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
    class MetodosSQLiteActividad
    {
        ///////////////////////////////////////////////////////
        //INSERTAR ACTIVIDAD

        public string Insert(string dbPath, string nombreActividad, string descripcionActividad)
        {
            if (vacios(nombreActividad, descripcionActividad) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Actividad()
                        {
                            NombreActividad = nombreActividad,
                            DescripcionActividad = descripcionActividad
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
        
        public string Update(string dbPath,int idActividad, string nombreActividad, string descripcionActividad)
        {
            if (vacios(nombreActividad, descripcionActividad) == false)
            {
                  
                using (var db = new SQLiteConnection(dbPath))
                {
                    
                    var existing = db.Query<Actividad>("SELECT * FROM Actividad").Where(c => c.IdActividad == idActividad).FirstOrDefault();

                    if (existing != null) {
                        existing.NombreActividad = nombreActividad;
                        existing.DescripcionActividad = descripcionActividad;

                        db.RunInTransaction(() =>
                        {
                            db.Update(existing);
                        });
                        return "La Actividad con el ID : " + existing.IdActividad + " se actualiza con exito"; ;
                    }
                    return "La Actividad con ese Carnet no existe";
                }
            }
            else {
                return "Debe llenar todos los campos";
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Borrar
        public string[] Delete(string dbPath, int idActividad) {
            if (idActividad > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Actividad>("SELECT * FROM Actividad").Where(c => c.IdActividad == idActividad).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdActividad.ToString(), existing.NombreActividad.ToString(), existing.DescripcionActividad, "La Actividad anterior se elimino correctamente" };

                        db.RunInTransaction(() =>
                        {
                            db.Delete(existing);
                        });

                        return vec;
                    }
                    else {
                        string[] vec = new string[] { idActividad.ToString(), "", "", "La Actividad no existe" };
                        return vec;
                    }
                }
            }
            else {
                MessageBox.Show("No a digitado el idActividad a borrar");
                string[] vec = new string[] { "", "", "", "" };
                return vec;
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Consulta
        public string[] Consulta(string dbPath, int idActividad) {
            if (idActividad > 0) {
                using (var db = new SQLiteConnection(dbPath)) {
                    var existing = db.Query<Actividad>("SELECT * FROM Actividad").Where(c => c.IdActividad == idActividad).FirstOrDefault();

                    if (existing != null){
                        string[] vec = new string[] { existing.IdActividad.ToString(), existing.NombreActividad.ToString(), existing.DescripcionActividad.ToString() , "Encontrado"};
                        return vec;
                    }
                    else
                    {
                        string[] vec = new string[] { "", "", "", "", "La Actividad solicitada no existe" };
                        return vec;
                    }
                }
            }
            else
            {
                MessageBox.Show("ERROR debe digitar el Id de la Actividad");
                string[] vec = new string[] { "", "", "", "", "" };
                return vec;
            }
        }
        ///////////////////////////////////////////////////////
        //Campos Vacios

        public bool vacios(string nombreActividad, string descripcionActividad)
        {
            if (nombreActividad == String.Empty)
            {
                MessageBox.Show("ERROR ingrese un nombre para la Actividad");
                return true;
            }
            else {
                if (descripcionActividad == String.Empty)
                {
                    MessageBox.Show("ERROR falta que digite la Descripcion");
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        ///////////////////////////////////////////////////////
    }


}

