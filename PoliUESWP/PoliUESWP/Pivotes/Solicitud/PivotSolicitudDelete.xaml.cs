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
    public partial class PivotSolicitudDelete : PhoneApplicationPage
    {
        public PivotSolicitudDelete()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            MetodoSQLiteSolicitud op = new MetodoSQLiteSolicitud();
            MetodoSQLiteSolicitud det = new MetodoSQLiteSolicitud();

            if (txtidSolicitud.Text == String.Empty)
            {
                MessageBox.Show("Ingrese un ID");
            }
            else {
                String[] res = op.Delete(dbPath, Int32.Parse(txtidSolicitud.Text));
                String[] resDet = det.Delete(dbPath, Int32.Parse(txtidSolicitud.Text));
                if (res != null)
                {

                    MessageBox.Show(res[1] + "\n" + resDet[1]);
                    MostrarDatos();
                    pivotPrincipal.SelectedItem = itemDatos;
                }
                else {
                    MessageBox.Show("No Existe");
                }
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
        }

        private void listaSolicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Solicitud items = (ClasesPorTabla.Solicitud)listaSolicitudes.SelectedItem;

            txtidSolicitud.Text = items.IdSolicitud.ToString();

            pivotPrincipal.SelectedItem = itemDatos;
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Solicitud>().Where(c => c.IdSolicitud >= 0).ToList();
            listaSolicitudes.ItemsSource = pers;
        }
    }
}