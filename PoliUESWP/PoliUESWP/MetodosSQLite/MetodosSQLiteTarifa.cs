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
    class MetodosSQLiteTarifa
    {
        ///////////////////////////////////////////////////////
        //INSERTAR ACTIVIDAD

        public string Insert(string dbPath, int cantPersonas, double tarifaUnica)
        {
            if (vacios(cantPersonas, tarifaUnica) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Tarifa()
                        {
                            CantidadPersonas = cantPersonas,
                            TarifaUnica = tarifaUnica
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

        public string Update(string dbPath, int idTarifa, int cantPersonas, double tarifaUnica)
        {
            if (vacios(cantPersonas, tarifaUnica) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    var existing = db.Query<Tarifa>("SELECT * FROM Tarifa").Where(c => c.IdTarifa == idTarifa).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.IdTarifa = idTarifa;
                        existing.CantidadPersonas = cantPersonas;
                        existing.TarifaUnica = tarifaUnica;

                        db.RunInTransaction(() =>
                        {
                            db.Update(existing);
                        });
                        return "La Tarifa con el ID : " + existing.IdTarifa + " se actualiza con exito"; ;
                    }
                    return "La Tarifa con ese ID no existe";
                }
            }
            else {
                return "Debe llenar todos los campos";
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Borrar
        public string[] Delete(string dbPath, int idTarifa)
        {
            if (idTarifa > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Tarifa>("SELECT * FROM Tarifa").Where(c => c.IdTarifa == idTarifa).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdTarifa.ToString(), existing.CantidadPersonas.ToString(), existing.TarifaUnica.ToString(), "La Tarifa se elimino correctamente" };

                        db.RunInTransaction(() =>
                        {
                            db.Delete(existing);
                        });

                        return vec;
                    }
                    else {
                        string[] vec = new string[] { idTarifa.ToString(), "", "", "La Tarifa no existe" };
                        return vec;
                    }
                }
            }
            else {
                MessageBox.Show("No a digitado el idTarifa a borrar");
                string[] vec = new string[] { "", "", "", "" };
                return vec;
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Consulta
        public string[] Consulta(string dbPath, int idTarifa)
        {
            if (idTarifa > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<Tarifa>("SELECT * FROM Tarifa").Where(c => c.IdTarifa == idTarifa).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdTarifa.ToString(), existing.CantidadPersonas.ToString(), existing.TarifaUnica.ToString(), "Encontrado" };
                        return vec;
                    }
                    else
                    {
                        string[] vec = new string[] { "", "", "", "", "La Tarifa solicitada no existe" };
                        return vec;
                    }
                }
            }
            else
            {
                MessageBox.Show("ERROR debe digitar el Id de la Tarifa");
                string[] vec = new string[] { "", "", "", "", "" };
                return vec;
            }
        }
        ///////////////////////////////////////////////////////


        //Campos Vacios

        public bool vacios(int cantPersonas, double tarifaUnica)
        {
            if (cantPersonas <= 0)
            {
                MessageBox.Show("ERROR ingrese una cantidad de Personas validas");
                return true;
            }
            else {
                if (tarifaUnica < 0)
                {
                    MessageBox.Show("ERROR digite una Tarifa Valida");
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
