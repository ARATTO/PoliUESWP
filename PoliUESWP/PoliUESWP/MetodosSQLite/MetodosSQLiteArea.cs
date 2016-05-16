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
    class MetodosSQLiteArea
    {
        ///////////////////////////////////////////////////////
        //INSERTAR ACTIVIDAD

        public string Insert(string dbPath, int maxPersona, string nombreArea, string descArea)
        {
            if (vacios(maxPersona, nombreArea, descArea) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Area()
                        {
                            MaxPersonas = maxPersona,
                            NombreArea = nombreArea,
                            DescArea = descArea
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

        public string Update(string dbPath, int idArea, int maxPersona, string nombreArea, string descArea)
        {
            if (vacios(maxPersona, nombreArea, descArea) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    var existing = db.Query<Area>("SELECT * FROM Area").Where(c => c.IdArea == idArea).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.MaxPersonas = maxPersona;
                        existing.NombreArea = nombreArea;
                        existing.DescArea = descArea;

                        db.RunInTransaction(() =>
                        {
                            db.Update(existing);
                        });
                        return "La Area con el ID : " + existing.IdArea + " se actualiza con exito"; ;
                    }
                    return "La Area con ese ID no existe";
                }
            }
            else {
                return "Debe llenar todos los campos";
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Borrar
        public string[] Delete(string dbPath, int idArea)
        {
            if (idArea > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Area>("SELECT * FROM Area").Where(c => c.IdArea == idArea).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdArea.ToString(), existing.MaxPersonas.ToString(), existing.NombreArea, existing.DescArea, "La Area se elimino correctamente" };

                        db.RunInTransaction(() =>
                        {
                            db.Delete(existing);
                        });

                        return vec;
                    }
                    else {
                        string[] vec = new string[] { idArea.ToString(), "", "","", "El Area no existe" };
                        return vec;
                    }
                }
            }
            else {
                MessageBox.Show("No a digitado el idArea a borrar");
                string[] vec = new string[] { "", "", "", "" ,""};
                return vec;
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Consulta
        public string[] Consulta(string dbPath, int idArea)
        {
            if (idArea > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Area>("SELECT * FROM Area").Where(c => c.IdArea == idArea).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdArea.ToString(), existing.MaxPersonas.ToString(), existing.NombreArea, existing.DescArea, "Encontrado" };
                        return vec;
                    }
                    else
                    {
                        string[] vec = new string[] { "", "", "", "", "El Area solicitada no existe" };
                        return vec;
                    }
                }
            }
            else
            {
                MessageBox.Show("ERROR debe digitar el Id de la Area");
                string[] vec = new string[] { "", "", "", "", "" };
                return vec;
            }
        }
        ///////////////////////////////////////////////////////
        //Campos Vacios

        public bool vacios(int maxPersona, string nombreArea, string descArea)
        {
            if (descArea == String.Empty)
            {
                MessageBox.Show("ERROR ingrese una descipcion");
                return true;
            }
            else {
                if (nombreArea == String.Empty)
                {
                    MessageBox.Show("ERROR digite un nombre");
                    return true;
                }
                else
                {
                    if (maxPersona < 0)
                    {
                        MessageBox.Show("ERROR Maximo de Personas incorrecto");
                        return true;
                    }
                    else
                    {

                        return false;
                    }

                }
            }

        }
        ///////////////////////////////////////////////////////
    }
}
