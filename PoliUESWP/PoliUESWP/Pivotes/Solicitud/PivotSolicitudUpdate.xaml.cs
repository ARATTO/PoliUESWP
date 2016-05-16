using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLite;
using System.IO;
using Windows.Storage;
using PoliUESWP.MetodosSQLite;

namespace PoliUESWP.Pivotes.Solicitud
{
    public partial class PivotSolicitudUpdate : PhoneApplicationPage
    {
        public PivotSolicitudUpdate()
        {
            InitializeComponent();
            MostrarSolicitud();
            MostrarActividad();
            MostrarTarifa();
            MostrarArea();
        }

        ClasesPorTabla.Solicitud solActual = new ClasesPorTabla.Solicitud();

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Actividad items = (ClasesPorTabla.Actividad)listaActividades.SelectedItem;

            txtActividad.Text = items.IdActividad.ToString();

            pivotPrincipal.SelectedItem = itemSolicitud;
        }

        private void listaTarifas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Tarifa items = (ClasesPorTabla.Tarifa)listaTarifas.SelectedItem;

            txtTarifa.Text = items.IdTarifa.ToString();

            pivotPrincipal.SelectedItem = itemSolicitud;
        }

        private void listaArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Area items = (ClasesPorTabla.Area)listaArea.SelectedItem;

            txtArea.Text = items.IdArea.ToString();

            pivotPrincipal.SelectedItem = itemDetalle;
        }

        private void btnActividad_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemActividad;
        }

        private void btnTarifa_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemTarifa;
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemDetalle;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtActividad.Text = String.Empty;
            txtTarifa.Text = String.Empty;
            txtMotivo.Text = String.Empty;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            MetodoSQLiteSolicitud sol = new MetodoSQLiteSolicitud();
            MetodoSQLiteDetalleSolicitud det = new MetodoSQLiteDetalleSolicitud();


            if (txtMotivo.Text == String.Empty || txtActividad.Text == String.Empty || txtTarifa.Text == String.Empty)
            {
                MessageBox.Show("Debe Llenar todos los campos");
            }
            else
            {

                String resSol = sol.Update(dbPath, Int32.Parse(txtSolicitud.Text), txtMotivo.Text, solActual.Fecha, Int32.Parse(txtActividad.Text), Int32.Parse(txtTarifa.Text));

                int ultiSol = sol.ConsultaUltimo(dbPath);

                if (ultiSol == -1)
                {
                    MessageBox.Show("ERROR no existen Solicitudes");
                }
                else {

                    double cobroTotal;
                    int idA = Int32.Parse(txtArea.Text);
                    int idT = Int32.Parse(txtTarifa.Text);
                    //Obtener Area
                    var db = new SQLiteConnection(dbPath);
                    var Are = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea == idA).FirstOrDefault();
                    //Obtener Tarifa
                    var db1 = new SQLiteConnection(dbPath);
                    var Tar = db1.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa == idT).FirstOrDefault();

                    cobroTotal = Tar.TarifaUnica * Are.MaxPersonas;

                    String[] idDetalleActual = det.Consulta(dbPath, solActual.IdSolicitud);


                    String resDet = det.Update(dbPath, Int32.Parse(idDetalleActual[0]), fechaInicio.Value.ToString(), fechaFin.Value.ToString(), cobroTotal, ultiSol, idA);

                    if (resDet == String.Empty)
                    {
                        MessageBox.Show("Debe Llenar todos los campos");
                    }
                    else
                    {
                        MessageBox.Show("Detalle Solicitud Guardado con Exito");
                        NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
                    }

                }
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemSolicitud;
        }
        public void MostrarActividad()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Actividad>().Where(c => c.IdActividad >= 0).ToList();
            listaActividades.ItemsSource = pers;
        }
        public void MostrarTarifa()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa >= 0).ToList();
            listaTarifas.ItemsSource = pers;
        }
        public void MostrarArea()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea >= 0).ToList();
            listaArea.ItemsSource = pers;
        }
        public void MostrarSolicitud()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Solicitud>().Where(c => c.IdSolicitud >= 0).ToList();

            listaSolicitudes.ItemsSource = pers;
        }

        private void listaSolicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Solicitud items = (ClasesPorTabla.Solicitud)listaSolicitudes.SelectedItem;

            txtSolicitud.Text = items.IdSolicitud.ToString();
            ////////
            solActual = items;

            MetodoSQLiteSolicitud sol = new MetodoSQLiteSolicitud();
            MetodoSQLiteDetalleSolicitud det = new MetodoSQLiteDetalleSolicitud();


            String[] res = sol.Consulta(dbPath, Int32.Parse(txtSolicitud.Text));
            String[] resDet = det.Consulta(dbPath, Int32.Parse(txtSolicitud.Text));

            txtMotivo.Text = res[1];
            txtActividad.Text = res[3];
            txtTarifa.Text = res[4];

            fechaInicio.Value = Convert.ToDateTime(resDet[1]);
            fechaFin.Value = Convert.ToDateTime(resDet[2]);
            txtArea.Text = resDet[5];
            ////////
            pivotPrincipal.SelectedItem = itemSolicitud;
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemArea;
        }
    }
}