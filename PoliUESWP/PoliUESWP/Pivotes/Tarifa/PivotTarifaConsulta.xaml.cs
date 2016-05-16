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
using PoliUESWP.MetodosSQLite;
using SQLite;

namespace PoliUESWP.Pivotes.Tarifa
{
    public partial class PivotTarifaConsulta : PhoneApplicationPage
    {
        public PivotTarifaConsulta()
        {
            InitializeComponent();
            mostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtidTarifa.Text = String.Empty;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteTarifa op = new MetodosSQLiteTarifa();

            String[] res = op.Consulta(dbPath, Int32.Parse(txtidTarifa.Text));

            txtCantPersonas.Text = res[1];
            txtTarifaUnica.Text = res[2];

            if (res[0] != string.Empty)
            {
                mostrarDatosUnicos(Int32.Parse(res[0]));
                pivotPrincipal.SelectedItem = itemConsulta;
            }
            else
            {
                mostrarDatos();
                MessageBox.Show("La Tarifa no existe");
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuTarifa.xaml?", UriKind.Relative));
        }

        public void mostrarDatosUnicos(int idTarifa)
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa == idTarifa).ToList();
            listaTarifas.ItemsSource = pers;
        }

        public void mostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa > 0).ToList();
            listaTarifas.ItemsSource = pers;
        }

        private void listaTarifa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Tarifa items = (ClasesPorTabla.Tarifa)listaTarifas.SelectedItem;

            txtCantPersonas.Text = String.Empty;
            txtTarifaUnica.Text = String.Empty;

            pivotPrincipal.SelectedItem = itemConsulta;
            MessageBox.Show("Encontrado ...");
            mostrarDatosUnicos(items.IdTarifa);
        }
    }
}