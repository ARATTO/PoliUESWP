using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PoliUESWP.MetodosSQLite;
using System.IO;
using Windows.Storage;
using SQLite;

namespace PoliUESWP.Pivotes.Solicitud
{
    public partial class PivotSolicitudInsert : PhoneApplicationPage
    {
        public PivotSolicitudInsert()
        {
            InitializeComponent();

            MostrarActividad();
            MostrarTarifa();
            MostrarArea();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnActividad_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemActividad;
        }

        private void btnTarifa_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemTarifa;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
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

        private void listaArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Area items = (ClasesPorTabla.Area)listaArea.SelectedItem;

            txtArea.Text = items.IdArea.ToString();

            pivotPrincipal.SelectedItem = itemDetalle;
        }

        private void listaTarifas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Tarifa items = (ClasesPorTabla.Tarifa)listaTarifas.SelectedItem;

            txtTarifa.Text = items.IdTarifa.ToString();

            pivotPrincipal.SelectedItem = itemSolicitud;
        }

        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Actividad items = (ClasesPorTabla.Actividad)listaActividades.SelectedItem;

            txtActividad.Text = items.IdActividad.ToString();
           
            pivotPrincipal.SelectedItem = itemSolicitud;
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemSolicitud;
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

                String hoy = DateTime.Today.ToString();
                String resSol = sol.Insert(dbPath, txtMotivo.Text, hoy, Int32.Parse(txtActividad.Text), Int32.Parse(txtTarifa.Text));

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

                    String resDet = det.Insert(dbPath, fechaInicio.Value.ToString() , fechaFin.Value.ToString(), cobroTotal, ultiSol, idA);

                    if (resDet == String.Empty)
                    {
                        MessageBox.Show("Debe Llenar todos los campos");
                    }
                    else
                    {
                        MessageBox.Show("Solicitud Guardada con Exito");
                        NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
                    }

                }
            }
            

            

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

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            pivotPrincipal.SelectedItem = itemArea;
        }
        
        
    }
}