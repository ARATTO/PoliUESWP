﻿using PoliUESWP.ClasesPorTabla;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PoliUESWP.MetodosSQLite
{
    class MetodoSQLiteDetalleSolicitud
    {
        ///////////////////////////////////////////////////////
        //INSERTAR ACTIVIDAD

        public string Insert(string dbPath, string fechaInicio, string fechaFin, double cobroTotal, int idSolicitud, int idArea)
        {
            if (vacios( fechaInicio, fechaFin, cobroTotal, idSolicitud, idArea) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new DetalleSolicitud()
                        {
                            FechaInicio = fechaInicio,
                            FechaFin = fechaFin,
                            CobroTotal = cobroTotal,
                            IDSolicitud = idSolicitud,
                            IDArea = idArea
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

        public string Update(string dbPath, int idDetalleSolicitud, string fechaInicio, string fechaFin, double cobroTotal, int idSolicitud, int idArea)
        {
            if (vacios(fechaInicio, fechaFin, cobroTotal, idSolicitud, idArea) == false)
            {

                using (var db = new SQLiteConnection(dbPath))
                {

                    var existing = db.Query<DetalleSolicitud>("SELECT * FROM DetalleSolicitud").Where(c => c.IDSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.FechaInicio = fechaInicio;
                        existing.FechaFin = fechaFin;
                        existing.CobroTotal = cobroTotal;
                        existing.IDSolicitud = idSolicitud;
                        existing.IDArea = idArea;

                        db.RunInTransaction(() =>
                        {
                            db.Update(existing);
                        });
                        return "Detalle de la Solicitud con el ID : " + existing.IdDetalleSolicitud + " se actualizo con exito"; ;
                    }
                    return "No hay Detalle asociado con esa Solicitud";
                }
            }
            else {
                return "Debe llenar todos los campos";
            }
        }
        ///////////////////////////////////////////////////////
        //Metodo Consulta

        public string[] Consulta(string dbPath, int idSolicitud)
        {
            if (!(idSolicitud.ToString() == String.Empty) & idSolicitud > 0)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var existing = db.Query<DetalleSolicitud>("SELECT * FROM DetalleSolicitud").Where(c => c.IDSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] {
                            existing.IdDetalleSolicitud.ToString(),
                            existing.FechaInicio,
                            existing.FechaFin,
                            existing.CobroTotal.ToString(),
                            existing.IDSolicitud.ToString(),
                            existing.IDArea.ToString(),
                            "Encontrado"
                        };
                        return vec;
                    }
                    else
                    {
                        string[] vec = new string[] { "", "", "", "", "", "", "La Solicitud no existe" };
                        return vec;
                    }
                }
            }
            else
            {
                MessageBox.Show("ERROR debe digitar el Id del Detalle Solicitud");
                string[] vec = new string[] { "", "", "", "","", "", "", };
                return vec;
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
                    var existing = db.Query<DetalleSolicitud>("SELECT * FROM DetalleSolicitud").Where(c => c.IDSolicitud == idSolicitud).FirstOrDefault();

                    if (existing != null)
                    {
                        string[] vec = new string[] { existing.IdDetalleSolicitud.ToString(), "Detalle Solicitud se elimino correctamente" };

                        db.RunInTransaction(() =>
                        {
                            db.Delete(existing);
                        });

                        return vec;
                    }
                    else {
                        string[] vec = new string[] { idSolicitud.ToString(), "NO existe Detalle asociado a esta Solicitud" };
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
        //Campos Vacios

        public bool vacios(string fechaInicio, string fechaFin, double cobroTotal, int idSolicitud, int idArea)
        {
            if (fechaInicio == String.Empty)
            {
                MessageBox.Show("ERROR fecha de inicio vacia");
                return true;
            }
            else {
                if (fechaFin == String.Empty)
                {
                    MessageBox.Show("ERROR fecha de fin solicitud vacia");
                    return true;
                }
                else
                {
                    if (cobroTotal <= 0)
                    {
                        MessageBox.Show("ERROR el cobro total es incorrecto");
                        return true;
                    }
                    else {
                        if (idSolicitud.ToString() == String.Empty)
                        {
                            MessageBox.Show("ERROR idSolicitud es vacio");
                            return true;
                        }
                        else {
                            if (idArea.ToString() == String.Empty)
                            {
                                MessageBox.Show("ERROR idArea es vacio");
                                return true;
                            }
                            else {
                                return false;
                            }
                            
                        }
                    }

                }
            }

        }
        ///////////////////////////////////////////////////////
    }
}
