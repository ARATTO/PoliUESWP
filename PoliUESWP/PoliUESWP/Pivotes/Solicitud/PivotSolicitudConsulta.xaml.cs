using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Windows.Storage;
using SQLite;
using PoliUESWP.MetodosSQLite;

namespace PoliUESWP.Pivotes.Solicitud
{
    public partial class PivotSolicitudConsulta : PhoneApplicationPage
    {
        public PivotSolicitudConsulta()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MetodoSQLiteSolicitud sol = new MetodoSQLiteSolicitud();
            MetodoSQLiteDetalleSolicitud det = new MetodoSQLiteDetalleSolicitud();

            if (txtidSolicitud.Text == String.Empty)
            {
                MessageBox.Show("ERROR digite su un ID valido");
            }
            else {
                String[] res = sol.Consulta(dbPath, Int32.Parse(txtidSolicitud.Text));
                String[] resDet = det.Consulta(dbPath, Int32.Parse(txtidSolicitud.Text));

                if (res[0] != String.Empty)
                {
                    mostrarDatosUnicos(Int32.Parse(res[0]));
                    txtMotivo.Text = res[1];
                    txtFecha.Text = res[2];
                    txtActividad.Text = res[3];
                    txtTarifa.Text = res[4];

                    txtFechaInicio.Text = resDet[1];
                    txtFechaFin.Text = resDet[2];
                    txtMonto.Text = resDet[3];
                    txtArea.Text = resDet[5];

                    
                }
                else
                {
                    MostrarDatos();
                    MessageBox.Show("La Solicitud no existe");
                }
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
        }
        public void mostrarDatosUnicos(int idSolicitud)
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Solicitud>().Where(c => c.IdSolicitud == idSolicitud).ToList();
            listaSolicitudes.ItemsSource = pers;
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Solicitud>().Where(c => c.IdSolicitud > 0).ToList();
            listaSolicitudes.ItemsSource = pers;
        }
    }
}